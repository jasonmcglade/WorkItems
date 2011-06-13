using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public class WorkItemSearchResult
    {
        public int TotalWorkItemCount { get; set; }
        public IEnumerable<WorkItem> WorkItems { get; set; } 
    }
}
