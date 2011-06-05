using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItems.Core.Domain;
using NHibernate;
using NHibernate.Linq;

namespace WorkItems.Core.Services
{
    public class WorkItemService : IWorkItemService
    {
        public ISession Session { get; private set; }

        public WorkItemService(ISession session)
        {
            Session = session;
        }

        public WorkItem[] GetAllWorkItems()
        {
            using (var transaction = Session.BeginTransaction())
            {
                var workItems = (from item in Session.Query<WorkItem>()
                                 select item).ToArray();

                transaction.Commit();

                return workItems;
            }
        }
    }
}
