using System;

namespace Software.Domain
{
    /// <summary>
    /// This class cleans poorly formatted version strings.
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// Cleans the passed version string and returns a Version object.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Version GetVersion(string softwareVersion)
        {
            return new Version(CleanVersionString(softwareVersion));
        }

        /// <summary>
        /// This methods removes extra unnecessary characters and cleans the data.
        /// This can be used to clean poorly persisted data or poorly entered data.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string CleanVersionString(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return null;

            // Remove spaces
            version = version.Replace(" ", string.Empty);

            // Fix missing 0 issues.
            // Missing 0 in front.
            version = (version[0] == '.') ? $"0{version}" : version;
            // Missing 0 at end.
            version = (version[version.Length-1] == '.') ? $"{version}0" : version;
            // Missing 0 between dots.
            version = version.Replace("..", ".0.");
            // No minor specified
            version = version.IndexOf('.') == -1 ? $"{version}.0" : version;

            return version;
        }
    }
}
