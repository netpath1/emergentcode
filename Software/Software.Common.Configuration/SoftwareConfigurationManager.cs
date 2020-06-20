using Microsoft.Extensions.Configuration;

namespace Software.Common.Configuration
{
    /// <summary>
    /// This class reads an IConfiguration from appsettings.json
    /// </summary>
    public class SoftwareConfigurationManager
    {
        private static IConfiguration _configuration = null;

        /// <summary>
        /// Read the appsettings.json and return an IConfiguration
        /// </summary>
        /// <returns></returns>
        public IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false, false)
                    .Build();
            }

            return _configuration;
        }
    }
}
