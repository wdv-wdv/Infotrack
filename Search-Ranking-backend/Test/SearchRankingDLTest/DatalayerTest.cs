using AutoMapper;
using SearchRankingBL;
using SearchRankingDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingDLTest
{
    [TestClass]
    public class DatalayerTest
    {
        [TestMethod]
        public void AutoMapper()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfileDL>();
            });

            // Act
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void GetSearchEngines()
        {
            //Act
            IDatalayer datalayer = new Datalayer();
            var results = datalayer.GetSearchEngines();
        }

        [TestMethod]
        public void GetSearches()
        {
            //Act
            IDatalayer datalayer = new Datalayer();
            var results = datalayer.GetSearches(false);
        }

        [DataTestMethod]
        [DataRow("land registry search")]
        [DataRow("cheese")]
        public void SaveSearch(string searchTerm)
        {
            // Arrange
            var search = new Search()
            {
                SearchTerm = searchTerm,
                URLs = new string[] { "www.infotrack.co.uk" }
            };

            // Act
            IDatalayer datalayer = new Datalayer();
            datalayer.SaveSearch(search);

            // Assert
            Assert.IsTrue(search.SearchTermID > 0);
        }


        [TestMethod]
        public void SaveSearchResults()
        {
            // Arrange
            var search = new Search()
            {
                SearchTermID = 1
            };

            var searchEngine = new SearchEngine()
            {
                ID = 1
            };

            var count = 10;

            //Act
            IDatalayer datalayer = new Datalayer();
            datalayer.SaveSearchResult(search, searchEngine, count);
        }

        [TestMethod]
        public void GetHistory()
        {
            //Act
            IDatalayer datalayer = new Datalayer();
            var results = datalayer.GetHistory();
        }
    }
}
