using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingBL
{
    /// <summary>
    /// Define IScarper methods
    /// </summary>
    public interface IScarper
    {
        public (int count, string? errorMessage) PreformLookup(Search search, SearchEngine seacrhEngine);
    }
}
