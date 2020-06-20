using System.Collections.Generic;

namespace Software.Data.Managers
{
    /// <summary>
    /// Provides access to stored software data.
    /// </summary>
    public interface ISoftwareDataManager
    {
        IEnumerable<Software> GetAllSoftware();
    }
}
