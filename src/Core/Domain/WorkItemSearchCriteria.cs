using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace WorkItems.Core.Domain
{
    public class WorkItemSearchCriteria
    {
        public const int PageSize = 5;

        public int Page { get; set; }
        public string Sort { get; set; }
        public string SortDir { get; set; }

        /// <summary>
        /// Apply the paging criteria to the provided query
        /// </summary>
        /// <param name="query">The current query to apply a paging filter to</param>
        /// <returns>An updated query with the paging applied</returns>
        public IQueryable<WorkItem> ApplyPaging(IQueryable<WorkItem> query)
        {
            query = query.Take(PageSize).Skip(PageSize * (Page - 1));
            return query;
        }

        /// <summary>
        /// Apply the sorting criteria to the provided query
        /// </summary>
        /// <param name="query">The current query to apply sorting to</param>
        /// <returns>An updated query with the sorting applied</returns>
        public IQueryable<WorkItem> ApplySorting(IQueryable<WorkItem> query)
        {
            if (string.IsNullOrEmpty(Sort))
            {
                return query;
            }

            if (ShouldSortDescending())
            {
                query = query.OrderByDescending(GetSortColumnByName(Sort));
            }
            else
            {
                query = query.OrderBy(GetSortColumnByName(Sort));
            }

            return query;
        }

        private bool ShouldSortDescending()
        {
            return SortDir == "DESC";
        }

        private Expression<Func<WorkItem, object>> GetSortColumnByName(string columnName)
        {
            var columnMappings = new Dictionary<string, Expression<Func<WorkItem, object>>>
            {
                {"Id", row => row.Id},
                {"Title", row => row.Title},
                {"Description", row => row.Description},
                {"Created Date", row => row.CreatedDate}
            };

            return columnMappings[columnName];
        }
    }
}
