using System.ComponentModel.DataAnnotations;

namespace Software.Shared
{
    public class SoftwareSearchItem
    {
        #region Public Properties

        public string Name { get; set; }

        [RegularExpression("^(\\d+\\.)?(\\d+\\.)?(\\d+\\.)?(\\*|\\d+)$", ErrorMessage = "Version filter is invalid.")]
        public string Version { get; set; }

        #endregion 
    }
}
