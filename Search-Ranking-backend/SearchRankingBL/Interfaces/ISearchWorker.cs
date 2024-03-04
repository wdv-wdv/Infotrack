using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingBL
{
    /// <summary>
    /// Define ISearchWorker methods
    /// </summary>
    public interface ISearchWorker
    {
        public int PerformSearch(Search search, SearchEngine searchEngine);
    }
}
