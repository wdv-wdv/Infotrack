using System;
using System.Collections.Generic;

namespace SearchRankingDL.EFmodels
{
    public partial class SearchTerm_EF
    {
        public SearchTerm_EF()
        {
            SearchResults_EF = new HashSet<SearchResult_EF>();
        }

        public int Id { get; set; }
        public string SearchTerm1 { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<SearchResult_EF> SearchResults_EF { get; set; }
    }
}
