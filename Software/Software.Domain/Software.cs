using System;

namespace Software.Domain
{
    public class Software
    {
        #region Constructors

        public Software() { }
        public Software(string name, string version)
        {
            Name = name;
            SetVersion(version);
        }

        #endregion Constructors

        #region Public Properties

        public string Name { get; set; }
        public Version Version { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void SetVersion(string version)
        {
            Version = VersionHelper.GetVersion(version);
        }

        #endregion Public Methods
    }
}
