using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Software.Data.Managers.Tests
{
    [TestClass]
    public class SoftwareDataManagerTests
    {
        [TestMethod]
        public void GetAllSoftwareTest()
        {
            var allSoftware = SoftwareManager.GetAllSoftware().ToList();

            // Fixed/hard-coded software manager so the data is consistent for the unit test.
            Assert.IsTrue(allSoftware.Count == 9);

            allSoftware.ForEach(o => Assert.IsFalse(string.IsNullOrWhiteSpace(o.Name)));
            allSoftware.ForEach(o => Assert.IsFalse(string.IsNullOrWhiteSpace(o.Version)));
        }

        [TestMethod]
        public void GetAllSoftwareFromFactoryManagerTest()
        {
            var dataManager = new SoftwareDataManagerFactory().CreateSoftwareDataManager();

            var allSoftware = dataManager.GetAllSoftware().ToList();

            // now get all software from static to compare
            var allSoftware2 = SoftwareManager.GetAllSoftware().ToList();

            Assert.IsTrue(allSoftware.Count == allSoftware2.Count);

            allSoftware.ForEach(o => 
                Assert.IsTrue(allSoftware2.Any(x =>
                {
                    return o.Name == x.Name
                        && o.Version == x.Version;
                })));
        }
    }
}
