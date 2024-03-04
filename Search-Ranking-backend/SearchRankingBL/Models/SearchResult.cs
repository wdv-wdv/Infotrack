using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingBL
{
    /// <summary>
    /// Dataclass for retrieving search results
    /// </summary>
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public string SearchEngine { get; set; }
        public int Ranking { get; set; }
        public DateTime Time { get; set; }
    }
}
