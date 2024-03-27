using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingBL
{
    public class SearchWorker(IDatalayer datalayer, IScarper scarper ) :ISearchWorker
    {
        public IDatalayer Datalayer { get; } = datalayer;
        public IScarper Scarper { get; } = scarper;

        /// <summary>
        /// Perform search using a IScarper
        /// Save search term & URL to database
        /// Save ranging result to database
        /// </summary>
        /// <param name="search"></param>
        /// <param name="searchEngine"></param>
        /// <returns>int: the search ranging</returns>
        /// <exception cref="Exception"></exception>
        public int PerformSearch(Search search, SearchEngine searchEngine) {

            var (count, error) = Scarper.PreformLookup(search, searchEngine);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception (error);
            }

            Datalayer.SaveSearch(search);
            Datalayer.SaveSearchResult(search, searchEngine, count);

            return count;
        }
    }
}
