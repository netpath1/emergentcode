using System.Collections.Generic;

namespace Software.Domain.Managers
{
    /// <summary>
    /// Provides search capabilities for software details.
    /// </summary>
    public interface ISoftwareManager
    {
        /// <summary>
        /// Get software matching requested criteria
        /// </summary>
        /// <param name="nameFilter">Name contains pass filter</param>
        /// <param name="versionFilter">Version filter for comparison</param>
        /// <param name="versionComparision">Version comparison type</param>
        /// <param name="maxCount">The maximum number of results to return</param>
        /// <returns></returns>
        IEnumerable<Software> GetSoftware(
            string nameFilter = null,
            string versionFilter = null,
            VersionComparison versionComparision = VersionComparison.EqualTo,
            int maxCount = 100);
    }

    /// <summary>
    /// Version comparison options
    /// </summary>
    public enum VersionComparison
    {
        EqualTo = 0,
        GreaterThanOrEqualTo = 1,
        GreaterThan = 2,
        LessThanOrEqualTo = 3,
        LessThan = 4,
    }
}
