using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItems.Core.Domain;

namespace WorkItems.Core.Services
{
    public interface IWorkItemService
    {
        WorkItem[] GetAllWorkItems();
    }
}
