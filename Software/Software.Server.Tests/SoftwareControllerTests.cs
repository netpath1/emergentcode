using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Software.Domain.Managers;
using Software.Server.Controllers;
using System;
using System.Linq;

namespace Software.Server.Tests
{
    [TestClass]
    public class SoftwareControllerTests
    {
        SoftwareManager softwareManager;
        ILogger<SoftwareController> logger;

        [TestInitialize]
        public void Init()
        {
            logger = new LoggerFactory().CreateLogger<SoftwareController>();
            softwareManager = new SoftwareManager();
        }

        [TestMethod]
        public void SearchSoftwareTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            var softwareResults = controller.SearchByVersion("1.0").ToList();
            // Known count with fixed hard code data manager as configured.
            Assert.IsTrue(softwareResults.Count == 7);

            softwareResults = controller.SearchByVersion("9999").ToList();
            Assert.IsTrue(softwareResults.Count == 0);

            softwareResults = controller.SearchByVersion("0.0").ToList();
            Assert.IsTrue(softwareResults.Count == 9);

            softwareResults = controller.SearchByVersion("2019.").ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            softwareResults = controller.SearchByVersion("2019.0").ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            softwareResults = controller.SearchByVersion("2019.1").ToList();
            Assert.IsTrue(softwareResults.Count == 0);
        }


        [TestMethod]
        public void SearchSoftwareExceptionTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            Assert.ThrowsException<HttpResponseException>(() =>
            {
                controller.SearchByVersion("1.0.abc").ToList();
            });
        }
    }
}
