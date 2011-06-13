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

        /// <summary>
        /// Search for Work Items using the specified criteria to filer, sort and apply paging
        /// </summary>
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

        /// <summary>
        /// Load the specified Work Item from the db
        /// </summary>
        public WorkItem GetById(int id)
        {
            return Session.Get<WorkItem>(id);
        }

        /// <summary>
        /// Update an existing Work Item (if present) or save a new Work Item
        /// </summary>
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
