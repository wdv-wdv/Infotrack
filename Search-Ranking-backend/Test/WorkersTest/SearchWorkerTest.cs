using ScarperSelenium;
using SearchRankingBL;
using SearchRankingDL;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkersTest
{
    [TestClass]
    public class SearchWorkerTest
    {
        [TestMethod]
        public void PerformSearch()
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new Search()
            {
                SearchTerm = "land registry search",
                URLs = new string[] { "www.infotrack.co.uk" }
            };

            var searchEngine = new SearchEngine()
            {
                ID = 1,
                Name = "Google",
                BaseURL = "https://www.google.co.uk/search?num=100&q=",
                CSSselectors = new string[]
                {
                    ".hlcw0c > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > span:nth-child(1) > a:nth-child(1) > div:nth-child(3) > div:nth-child(1) > div:nth-child(2) > div:nth-child(2) > cite:nth-child(1)",
                    "#rso > div:nth-child(3) > div > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > span:nth-child(1) > a:nth-child(1) > div:nth-child(3) > div:nth-child(1) > div:nth-child(2) > div:nth-child(2) > cite:nth-child(1)"
                }
            };

            // Act
            IScarper scarper = new Scarper(arr.logger);
            var searchWorker = new SearchWorker(arr.datalayer, scarper);
            searchWorker.PerformSearch(search, searchEngine);
        }
    }
}
