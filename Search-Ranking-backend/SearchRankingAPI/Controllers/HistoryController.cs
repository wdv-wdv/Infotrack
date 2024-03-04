using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SearchRankingBL;

namespace SearchRankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController(Serilog.ILogger logger, IDatalayer datalayer) : Controller
    {
        private readonly Serilog.ILogger logger = logger.ForContext<HistoryController>();
        private readonly IDatalayer datalayer = datalayer;

        [HttpGet(Name = "GetHistory")]
        public IEnumerable<SearchResult> Get()
        {
            return datalayer.GetHistory();
        }
    }
}
