using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Software.Common.Configuration.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void GetSettingTest()
        {
            var configManager = new SoftwareConfigurationManager();

            var setting = configManager.GetConfiguration()["SoftwareDataManagerType"];

            Assert.IsTrue(setting == "class1, assemblyA");

            setting = configManager.GetConfiguration()["SomeMissingSetting"];

            Assert.IsNull(setting);
        }
    }
}
