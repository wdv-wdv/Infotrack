using SearchRankingAPI.Controllers;
using SearchRankingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersTest
{
    [TestClass]
    public class SearchControllerTest
    {
        [DataTestMethod]
        [DataRow("land registry search", true)]
        [DataRow("cheese", false)]
        public void Post(string searchTerm, bool shouldFound)
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new SearchDto()
            {
                SearchTerm = searchTerm,
                URL = "www.infotrack.co.uk"
            };
            
            //Act
            var controller = new PerformSearchController(arr.logger,arr.searchWorker,arr.datalayer, arr.autoMapperAPI);
            var count = controller.Post(search);

            // Assert
            Assert.IsTrue(
                shouldFound & count > 0 |  //should found, count > 0
                !shouldFound & count == 0  //should not found, count == 0
            );
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Post_NoSearchTerm()
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new SearchDto()
            {
                SearchTerm = "",
                URL = "www.infotrack.co.uk"
            };

            //Act
            var controller = new PerformSearchController(arr.logger, arr.searchWorker, arr.datalayer, arr.autoMapperAPI);
            var count = controller.Post(search);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Post_NoURl()
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new SearchDto()
            {
                SearchTerm = "land registry search",
                URL = ""
            };

            //Act
            var controller = new PerformSearchController(arr.logger, arr.searchWorker, arr.datalayer, arr.autoMapperAPI);
            var count = controller.Post(search);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Post_WrongURl()
        {
            // Arrange
            var arr = new Test.TestArrangements();
            var search = new SearchDto()
            {
                SearchTerm = "land registry search",
                URL = "wwwinfotrackcouk"
            };

            //Act
            var controller = new PerformSearchController(arr.logger, arr.searchWorker, arr.datalayer, arr.autoMapperAPI);
            var count = controller.Post(search);
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            var arr = new Test.TestArrangements();

            //Act
            var controller = new PerformSearchController(arr.logger, arr.searchWorker, arr.datalayer, arr.autoMapperAPI);
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Its all good", result);
        }
    }
}
