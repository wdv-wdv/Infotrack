using System;
using System.Collections.Generic;

namespace SearchRankingDL.EFmodels
{
    public partial class SearchResult_EF
    {
        public int Id { get; set; }
        public int SearchEngineId { get; set; }
        public int SearchTermId { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public virtual SearchEngine_EF SearchEngine_EF { get; set; } = null!;
        public virtual SearchTerm_EF SearchTerm_EF { get; set; } = null!;
    }
}
