using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SearchRankingBL;
using SearchRankingDL.EFmodels;


namespace SearchRankingDL
{
    /// <summary>
    /// Datalayer base on IDatalayer
    /// </summary>
    public class Datalayer : IDatalayer
    {
        private SearchRankingContext SearchRankingContext;
        private IMapper mapper;
        public Datalayer() {
            this.SearchRankingContext = new SearchRankingContext();

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfileDL>();
            });

#if DEBUG
            config.AssertConfigurationIsValid();
#endif
            mapper = config.CreateMapper();
        }

        /// <summary>
        /// Return all search engines define in the database
        /// </summary>
        /// <returns>SearchEngine[]</returns>
        public SearchEngine[] GetSearchEngines()
        {
            var searchEngines = SearchRankingContext.SearchEngines
                .Include(s => s.SearchEngineCsses_EF)
                .Select(s => mapper.Map<SearchEngine>(s))
                .ToArray();

            return searchEngines;
        }

        /// <summary>
        /// Return array of searches based on the value form the SearchTerm and URLs tables
        /// </summary>
        /// <param name="activeOnly"></param>
        /// <returns>Search[]</returns>
        public Search[] GetSearches(bool activeOnly)
        {
            var urls = SearchRankingContext.Urls
                .Select(u => mapper.Map<string>(u))
                .ToArray();

            var searches = SearchRankingContext.SearchTerms
                .Where(s => !activeOnly || (activeOnly &  s.Active))
                .Select(s => mapper.Map(urls,mapper.Map<Search>(s)))
                .ToArray();
            return searches;
        }

        /// <summary>
        /// Using a Search object saves search terms and URLs to the database
        /// </summary>
        /// <param name="search"></param>
        public void SaveSearch(Search search)
        {
            foreach (var url in search.URLs)
            {
                if (!SearchRankingContext.Urls.Any(u => u.Url1 == url))
                {
                    var Url = mapper.Map<Url_EF>(url);
                    SearchRankingContext.Urls.Add(Url);
                }
            }

            var searchTerm = SearchRankingContext.SearchTerms
                .FirstOrDefault(s => s.SearchTerm1 == search.SearchTerm);

            if (searchTerm != null)
            {
                searchTerm.Active = true;
            }
            else
            {
                searchTerm = mapper.Map<SearchTerm_EF>(search);
                SearchRankingContext.SearchTerms.Add(searchTerm);
            }

            SearchRankingContext.SaveChanges();

            search.SearchTermID = searchTerm.Id;

        }

        /// <summary>
        /// Save the search ranging result (count) to database, as well refences to search term, search engine and time
        /// </summary>
        /// <param name="search"></param>
        /// <param name="searchEngine"></param>
        /// <param name="count"></param>
        public void SaveSearchResult(Search search, SearchEngine searchEngine, int count)
        {
            var searchResult = new SearchResult_EF()
            {
                SearchEngineId = searchEngine.ID,
                SearchTermId = search.SearchTermID,
                Date = DateTime.Now,
                Count = count
            };

            SearchRankingContext.SearchResults.Add(searchResult);
            SearchRankingContext.SaveChanges();

        }

        public SearchResult[] GetHistory()
        {
            return SearchRankingContext.SearchResults
                .Include(s => s.SearchEngine_EF)
                .Include(s => s.SearchTerm_EF)
                .Where(s => s.SearchTerm_EF.Active == true)
                .Take(100)
                .OrderByDescending(r => r.Date)
                .Select(r => mapper.Map<SearchResult>(r))
                .ToArray();
        }
    }
}
