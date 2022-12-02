using Schma.E3ProjectManager.Presentation.EventStoreWeb.Enumerations;

namespace Schma.E3ProjectManager.Presentation.EventStoreWeb.Models
{
    public class BranchPointModel
    {
        /// <summary>
        /// Branch point indicative name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the branch point
        /// </summary>
        public BranchPointType Type { get; set; }

        public List<RetroactiveEventModel> RetroactiveEvents { get; set; }
    }
}
