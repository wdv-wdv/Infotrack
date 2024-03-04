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
    public class HistoryControllerTest
    {
        [TestMethod]
        public void GetHistory()
        {
            // Arrange
            var arr = new Test.TestArrangements();

            //Act
            var controller = new HistoryController(arr.logger, arr.datalayer);
            var results = controller.Get();


        }
    }
}
