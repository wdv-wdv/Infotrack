using System;
using System.Collections.Generic;

namespace SearchRankingDL.EFmodels
{
    public partial class SearchEngine_EF
    {
        public SearchEngine_EF()
        {
            SearchEngineCsses_EF = new HashSet<SearchEngineCss_EF>();
            SearchResults_EF = new HashSet<SearchResult_EF>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;

        public virtual ICollection<SearchEngineCss_EF> SearchEngineCsses_EF { get; set; }
        public virtual ICollection<SearchResult_EF> SearchResults_EF { get; set; }
    }
}
