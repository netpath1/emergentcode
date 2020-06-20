using System.Collections.Generic;

namespace Software.Data.Managers
{
    /// <summary>
    /// This class exposes a ISoftwareDataManager for the hard coded data manager.
    /// This class can easily be replaced by updating the system configuration settings for setting: 
    /// This allows for the system to easily be updated to replace where the software data is coming.
    /// </summary>
    public class SoftwareHardCodedDataManager : ISoftwareDataManager
    {
        public IEnumerable<Software> GetAllSoftware()
        {
            // Wrapped for easy replacement of hard coded, static component.

            return SoftwareManager.GetAllSoftware();
        }
    }
}
