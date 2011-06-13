using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItems.Core.Domain;
using NHibernate;
using NHibernate.Linq;
using System.Transactions;

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

        public WorkItem GetById(int id)
        {
            return Session.Get<WorkItem>(id);
        }

        public void Save(SaveWorkItemDetails workItemDetails)
        {
            var repositoryWorkItem = GetById(workItemDetails.Id);

            if (repositoryWorkItem == null)
            {
                repositoryWorkItem = new WorkItem();
            }

            repositoryWorkItem.Title = workItemDetails.Title;
            repositoryWorkItem.Description = workItemDetails.Description;

            if (!string.IsNullOrEmpty(workItemDetails.AddedComment))
            {
                repositoryWorkItem.AddComment(workItemDetails.AddedComment, "unknown");
            }

            Session.SaveOrUpdate(repositoryWorkItem);
        }
    }
}
