using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingAPI.Models
{
    /// <summary>
    /// Dto data class used by controller for performing searches
    /// </summary>
    public class SearchDto
    {
        public string SearchTerm { get; set; }
        public string URL { get; set; }
    }
}
