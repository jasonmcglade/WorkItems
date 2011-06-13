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

        public WorkItemSearchResult GetWorkItemsByCriteria(WorkItemSearchCriteria criteria)
        {
            var query = Session.Query<WorkItem>();

            var totalCount = query.Count();

            query = criteria.ApplySorting(query);
            query = criteria.ApplyPaging(query);

            return new WorkItemSearchResult
                        {
                            WorkItems = query.ToArray(),
                            TotalWorkItemCount = totalCount
                        };
        }
    }
}
