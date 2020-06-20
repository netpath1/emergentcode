using Software.Data.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Software.Domain.Managers
{
    /// <summary>
    /// Provides software search capabilities based on the configured software data manager.
    /// </summary>
    public class SoftwareManager : ISoftwareManager
    {
        #region ISoftwareManager interface

        public IEnumerable<Software> GetSoftware(
            string nameFilter = null, 
            string versionFilter = null, 
            VersionComparison versionComparision = VersionComparison.EqualTo, 
            int maxCount = 100)
        {
            // Validate parameters
            if (maxCount < 1)
                throw new ArgumentException("maxCount must be 1 or greater");
            
            var allSoftware = GetAllSoftware();

            // No filtering
            if (string.IsNullOrWhiteSpace(nameFilter) && string.IsNullOrWhiteSpace(versionFilter))
            {
                return allSoftware.Take(maxCount);
            }

            var version = string.IsNullOrWhiteSpace(versionFilter) 
                ? null 
                : VersionHelper.GetVersion(versionFilter);

            return allSoftware
                .Where(o =>
                    CheckName(nameFilter, o.Name)
                    && CheckVersion(version, o.Version, versionComparision))
                .Take(maxCount);
        }

        #endregion ISoftwareManager interface


        #region Private Methods

        private IEnumerable<Software> GetAllSoftware()
        {
            var dataManager = new SoftwareDataManagerFactory().CreateSoftwareDataManager();

            var allSoftware = dataManager.GetAllSoftware();

            // Map and return domain objects from data objects.
            return allSoftware
                .Select(o => new Software(o.Name, o.Version));
        }

        private bool CheckName(string nameFilter, string name)
        {
            return string.IsNullOrWhiteSpace(nameFilter) || name.Contains(nameFilter);
        }

        private bool CheckVersion(Version versionFilter, Version checkVersion, VersionComparison versionFilterComparision)
        {
            if (versionFilter == null)
                return true;

            var compareResult = checkVersion.CompareTo(versionFilter);

            return (compareResult == 0
                        && (versionFilterComparision == VersionComparison.EqualTo
                            || versionFilterComparision == VersionComparison.GreaterThanOrEqualTo
                            || versionFilterComparision == VersionComparison.LessThanOrEqualTo))
                    || (compareResult < 0
                        && (versionFilterComparision == VersionComparison.LessThan
                            || versionFilterComparision == VersionComparison.LessThanOrEqualTo))
                    || (compareResult > 0
                        && (versionFilterComparision == VersionComparison.GreaterThan
                            || versionFilterComparision == VersionComparison.GreaterThanOrEqualTo));
        }

        #endregion Private Methods
    }
}