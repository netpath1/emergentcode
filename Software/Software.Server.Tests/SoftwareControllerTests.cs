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
        public void SearchSoftwareVersionTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            var searchParams = new Software.Shared.SoftwareSearchItem()
            {
                Version = "1.0"
            };

            var softwareResults = controller.Search(searchParams).ToList();
            // Known count with fixed hard code data manager as configured.
            Assert.IsTrue(softwareResults.Count == 7);

            searchParams.Version = "9999";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 0);

            searchParams.Version = "0.0";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 9);

            searchParams.Version = "2019.";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            searchParams.Version = "2019.0";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            searchParams.Version = "2019.1";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 0);
        }

        [TestMethod]
        public void SearchSoftwareNameTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            var searchParams = new Software.Shared.SoftwareSearchItem()
            {
                Name = "vis"
            };

            var softwareResults = controller.Search(searchParams).ToList();
            // Known count with fixed hard code data manager as configured.
            Assert.IsTrue(softwareResults.Count == 3);

            searchParams.Name = "code";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            searchParams.Name = "xyz";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 0);
        }

        [TestMethod]
        public void SearchSoftwareNameAndVersionTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            var searchParams = new Software.Shared.SoftwareSearchItem()
            {
                Name = "vis",
                Version = "1."

            };

            var softwareResults = controller.Search(searchParams).ToList();
            // Known count with fixed hard code data manager as configured.
            Assert.IsTrue(softwareResults.Count == 3);

            searchParams.Version = "2018.1";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 1);

            searchParams.Version = "2025.1";
            softwareResults = controller.Search(searchParams).ToList();
            Assert.IsTrue(softwareResults.Count == 0);
        }

        [TestMethod]
        public void SearchSoftwareExceptionTests()
        {
            var controller = new SoftwareController(logger, softwareManager);

            Assert.ThrowsException<HttpResponseException>(() =>
            {
                controller
                    .Search(
                        new Shared.SoftwareSearchItem()
                        {
                            Version = "1.0.abc"
                        })
                    .ToList();
            });
        }
    }
}
