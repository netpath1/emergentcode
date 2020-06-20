using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Software.Domain.Managers.Tests
{
    // These tests are consistent because the underlying data manager store is consistent, hard coded.

    [TestClass]
    public class SoftwareManagerTests
    {
        [TestMethod]
        public void GetAllSoftwareTest()
        {
            var manager = new SoftwareManager();

            var allSoftware = manager.GetSoftware().ToList();

            // This is true because the data manager is fixed/hard-coded.
            Assert.IsTrue(allSoftware.Count == 9);
        }

        [TestMethod]
        public void GetSoftwareByVersionFilterTests()
        {
            var manager = new SoftwareManager();

            var filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "2016.0", 
                    versionComparision: VersionComparison.GreaterThanOrEqualTo)
                .ToList();

            // This is true because the data manager is fixed/hard-coded.
            Assert.IsTrue(filteredSoftware.Count == 2);


            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "2019.0",
                    versionComparision: VersionComparison.GreaterThanOrEqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 1);


            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "2017.0.1",
                    versionComparision: VersionComparison.EqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 1);

            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "1",
                    versionComparision: VersionComparison.GreaterThanOrEqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 7);


            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "9999",
                    versionComparision: VersionComparison.LessThan)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 9);


            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "2017.0.1",
                    versionComparision: VersionComparison.LessThan)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 7);


            filteredSoftware = manager
                .GetSoftware(
                    versionFilter: "2017.0.1",
                    versionComparision: VersionComparison.LessThanOrEqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 8);
        }

        [TestMethod]
        public void GetSoftwareByNameAndVersionFilterTests()
        {
            var manager = new SoftwareManager();

            var filteredSoftware = manager
                .GetSoftware("Visual Studio", "2017.0.1", VersionComparison.EqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 1);


            filteredSoftware = manager
                .GetSoftware("Visual Studio INVALID", "2017.0.1", VersionComparison.EqualTo)
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 0);
        }

        [TestMethod]
        public void GetSoftwareByNameFilterTests()
        {
            var manager = new SoftwareManager();

            var filteredSoftware = manager
                .GetSoftware("Visual Studio")
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 3);


            filteredSoftware = manager
                .GetSoftware("Visual Studio Code")
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 1);


            filteredSoftware = manager
                .GetSoftware("INVALID TITLE")
                .ToList();

            Assert.IsTrue(filteredSoftware.Count == 0);
        }

        [TestMethod]
        public void GetSoftwareExceptionTests()
        {
            var manager = new SoftwareManager();

            Assert.ThrowsException<FormatException>(() =>
            {
                manager
                .GetSoftware(
                    versionFilter: "A.B",
                    versionComparision: VersionComparison.GreaterThanOrEqualTo)
                .ToList();
            });
        }
    }
}
