using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SearchRankingAPI.Models;
using SearchRankingBL;
using Serilog.Core;
using System.Text.RegularExpressions;

namespace SearchRankingAPI.Controllers;

/// <summary>
/// API controller for search actions
/// </summary>
/// <param name="logger"></param>
/// <param name="searchWorker"></param>
/// <param name="datalayer"></param>
/// <param name="mapper"></param>
[ApiController]
[Route("[controller]")]
public class PerformSearchController(Serilog.ILogger logger, ISearchWorker searchWorker, IDatalayer datalayer, IMapper mapper) : ControllerBase
{
    private readonly Serilog.ILogger logger = logger.ForContext<PerformSearchController>();
    private readonly ISearchWorker searchWorker = searchWorker;
    private readonly IDatalayer datalayer = datalayer;
    public static Regex urlRegex = new Regex(@"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,6}$", RegexOptions.Compiled);

    /// <summary>
    /// Get request endpoint to show backend system is up and run
    /// </summary>
    /// <returns>String</returns>
    [HttpGet(Name = "Get")]
    public string Get()
    {
        return "Its all good";
    }

    /// <summary>
    /// Perform search using a IScarper
    /// Save search term & URL to database
    /// Save ranging result to database
    /// </summary>
    /// <param name="search"></param>
    /// <returns>int count</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost(Name = "PerformSearch")]
    public int Post(SearchDto search)
    {
        // Clean up & verify
        search.SearchTerm = search.SearchTerm.Trim();
        search.URL = search.URL.Trim();

        if(search.SearchTerm.Length == 0)
        {
            throw new Exception("SearchTerm value invalid");
        }

        Uri uri;
        if (search.URL.Length == 0 || !urlRegex.IsMatch(search.URL))
        {
            throw new Exception("URL value invalid");
        }

        var searchEngine = datalayer.GetSearchEngines().FirstOrDefault(); 
        if( searchEngine == null ) {
            throw new Exception("No search engines defined");
        }

        // Act
        try
        {
            var result = searchWorker.PerformSearch(mapper.Map<Search>(search), searchEngine);
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
