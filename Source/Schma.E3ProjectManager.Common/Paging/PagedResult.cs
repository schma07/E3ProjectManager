//https://www.codingame.com/playgrounds/5363/paging-with-entity-framework-core
using System.Collections.Generic;

namespace Schma.E3ProjectManager.Common
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}