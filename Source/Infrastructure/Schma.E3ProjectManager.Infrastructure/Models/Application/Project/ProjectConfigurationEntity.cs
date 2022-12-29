using Schma.Data.Abstractions;

// TODO: Declare conventions for description; All kind of project related entities should be represented (Functions, locations, options, variants and
//       possibly even Attributmapping to E3Project

namespace Schma.E3ProjectManager.Infrastructure.Models
{
    /// <summary>
    /// Data entity for project-specific configurations
    /// </summary>
    public class ProjectConfigurationEntity : DataEntityBase<string>
    {
        /// <summary>
        /// Description of the configuration variable.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Value of the configuration variable.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Indicates whether the <see cref="Value"/> field contains an encrypted text.
        /// </summary>
        public bool IsEncrypted { get; set; }
    }
}