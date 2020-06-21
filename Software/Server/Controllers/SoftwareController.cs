using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Software.Domain.Managers;
using Software.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Software.Server.Controllers
{
    /// <summary>
    /// Software API controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        private readonly ILogger<SoftwareController> logger;
        private readonly ISoftwareManager softwareManager;

        public SoftwareController(ILogger<SoftwareController> logger, ISoftwareManager softwareManager)
        {
            this.logger = logger;
            this.softwareManager = softwareManager;
        }

        [HttpPost]
        [Route("Search")]
        public IEnumerable<SoftwareSearchItem> Search(SoftwareSearchItem softwareSearchItem)
        {
            try
            {
                var results = softwareManager
                    .GetSoftware(
                        softwareSearchItem.Name,
                        softwareSearchItem.Version,
                        versionComparision: VersionComparison.GreaterThan)
                    .OrderBy(o => o.Name)
                    .ThenBy(o => o.Version)
                    .Select(o => 
                        new SoftwareSearchItem()
                        {
                            Name = o.Name,
                            Version = o.Version.ToString()
                        })
                    .ToList();

                return results;
            }
            catch(Exception ex)
            {
                var msg = "An error occurred retrieving software list.";

                // log and/or notify of system exception
                logger.LogError(ex, msg);

                // return end user error message
                throw new HttpResponseException(msg);
            }
        }
    }
}
