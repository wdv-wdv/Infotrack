using SearchRankingAPI.Models;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ControllersTest
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod]
        public void AutoMapperConfig()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfileAPI>();
            });

            //Act
            config.AssertConfigurationIsValid();
        }
    }
}
