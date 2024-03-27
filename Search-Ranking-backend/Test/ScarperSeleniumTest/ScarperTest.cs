using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using ScarperSelenium;
using SearchRankingBL;
using Serilog;
using Serilog.Core;

namespace ScarperSeleniumTest
{
    [TestClass]
    public class ScarperTest
    {
        [DataTestMethod]
        [DataRow("land registry search", true)]
        [DataRow("cheese", false)]
        public void Process(string searchTerm, bool shouldFound)
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new Search()
            {
                SearchTerm = searchTerm,
                URLs = new string[] { "www.infotrack.co.uk" }
            };

            var searchEngine = new SearchEngine()
            {
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
            var (count, errorMessage) = scarper.PreformLookup(search, searchEngine);

            // Assert
            Assert.IsNull(errorMessage); //no errors report.
            Assert.IsTrue(
                shouldFound & count > 0 |  //should found, count > 0
                !shouldFound & count == 0  //should not found, count == 0
            );
        }
    }
}