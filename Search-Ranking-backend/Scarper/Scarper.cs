using Serilog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.DevTools.V120.CSS;
using OpenQA.Selenium.Edge;
using SearchRankingBL;
using System.Web;
using OpenQA.Selenium.Chrome;

namespace ScarperSelenium
{
    /// <summary>
    /// IScarper based on Selenium
    /// </summary>
    /// <param name="logger"></param>
    public class Scarper(ILogger logger) : IScarper, IDisposable
    {
        private EdgeDriver browser;
        private ILogger logger = logger.ForContext<Scarper>();

        /// <summary>
        /// Scraps search engine to see if URL are present for a search term
        /// </summary>
        /// <param name="search"></param>
        /// <param name="searchEngine"></param>
        /// <returns>
        ///     int count: ranging on the search result page, 0 when not found.
        ///     string? errorMessage: when browser fails
        /// </returns>
        public (int count, string? errorMessage) Process(Search search, SearchEngine searchEngine)
        {
            try
            {
                var options = new EdgeOptions();
                options.AddArguments("--headless");
                var edgeDriverService = EdgeDriverService.CreateDefaultService();
                edgeDriverService.HideCommandPromptWindow = true;
                edgeDriverService.SuppressInitialDiagnosticInformation = true;
                browser = new EdgeDriver(edgeDriverService,options);
               
                logger.Information($"Search term: {search.SearchTerm}");
                var landingURL = $"{searchEngine.BaseURL}{HttpUtility.UrlEncode(search.SearchTerm)}";
                logger.Information($"Landing URL: {landingURL}");
                browser.Navigate().GoToUrl(landingURL);

                int count = 0;

                List<IWebElement> resultElements = new List<IWebElement> ();

                foreach (var cs in searchEngine.CSSselectors)
                {
                    resultElements.AddRange(browser.FindElements(By.CssSelector(cs)));
                    foreach (var el in resultElements)
                    {
                        count++;
                        var s = el.GetAttribute("innerText");
                        logger.Verbose(s);
                        foreach (var url in search.URLs)
                        {
                            if (s.Contains(url))
                            {
                                logger.Information($"Result found at {count}");
                                return (count, null);
                            }
                        }
                    }
                }
                

                logger.Information($"URL not find within the first {search.MaxResults} results");
                return (0, null);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return (0, "Error while searching");
            }
        }

        public void Dispose()
        {
            if (this.browser != null)
            {
                this.browser.Dispose();
            }
        }
    }
}
