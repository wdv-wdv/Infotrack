

namespace SearchRankingBL
{
    /// <summary>
    /// Define IDatalayer methods 
    /// </summary>
    public interface IDatalayer
    {
        public SearchEngine[] GetSearchEngines();
        public Search[] GetSearches(bool activeOnly);
        public void SaveSearch(Search search);
        public void SaveSearchResult(Search search, SearchEngine searchEngine, int count);
        public SearchResult[] GetHistory();
    }
}
