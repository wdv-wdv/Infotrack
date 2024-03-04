using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingBL
{
    /// <summary>
    /// Data class used by search function
    /// </summary>
    public class SearchEngine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public string[] CSSselectors { get; set; }
    }
}
