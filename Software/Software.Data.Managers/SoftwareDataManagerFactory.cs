using System;

namespace Software.Data.Managers
{
    /// <summary>
    /// This factory creates managers based on the current configuration.
    /// </summary>
    public class SoftwareDataManagerFactory
    {
        /// <summary>
        /// This method creates in an object instance from the configuration setting: "SoftwareDataManagerType".
        /// </summary>
        /// <returns></returns>
        public ISoftwareDataManager CreateSoftwareDataManager()
        {
            var configuration = new Common.Configuration.SoftwareConfigurationManager();

            var setting = configuration.GetConfiguration()["SoftwareDataManagerType"];

            return (ISoftwareDataManager) Activator.CreateInstance(Type.GetType(setting));
        }
    }
}
