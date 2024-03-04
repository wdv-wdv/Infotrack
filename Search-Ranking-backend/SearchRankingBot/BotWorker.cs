using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SearchRankingBL;
using Serilog;
using Serilog.Core;

namespace SearchRankingBot
{
    /// <summary>
    /// Get a list of all active search terms in database
    /// Perform search on each search terms and save results in database
    /// </summary>
    /// <param name="hostApplicationLifetime"></param>
    /// <param name="logger"></param>
    /// <param name="datalayer"></param>
    /// <param name="searchWorker"></param>
    internal class BotWorker(IHostApplicationLifetime hostApplicationLifetime, ILogger logger, IDatalayer datalayer, ISearchWorker searchWorker) : BackgroundService
    {
        private readonly ILogger logger = logger;
        private readonly IHostApplicationLifetime hostApplicationLifetime = hostApplicationLifetime;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.Information("Search Ranking Bot Starts");
            
            var searchEgine = datalayer.GetSearchEngines().FirstOrDefault();
            if(searchEgine == null)
            {
                logger.Error("No search engine defined");
                Environment.Exit(1);
            }
            var searches = datalayer.GetSearches(true);

            logger.Information($"Search terms found: {searches.Length}");

            foreach(var search in searches)
            {
                logger.Information($"Performing search for : {search.SearchTerm} ");
                var count = searchWorker.PerformSearch(search, searchEgine);
                if (count > 0)
                {
                    logger.Information($"URL found at {count} on {searchEgine.Name}");
                }
                else
                {
                    logger.Warning($"URL not found in the first {search.MaxResults} results");
                }
            }

            logger.Information("Searches complete");
            Environment.Exit(0);
        }
    }
}
