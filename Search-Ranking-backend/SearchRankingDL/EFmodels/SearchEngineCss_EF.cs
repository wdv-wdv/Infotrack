using System;
using System.Collections.Generic;

namespace SearchRankingDL.EFmodels
{
    public partial class SearchEngineCss_EF
    {
        public int Id { get; set; }
        public int SearchEngineId { get; set; }
        public string Cssselector { get; set; } = null!;

        public virtual SearchEngine_EF SearchEngine_EF { get; set; } = null!;
    }
}
