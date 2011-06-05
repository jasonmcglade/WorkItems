using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using WorkItems.Core.Domain;

namespace WorkItems.Core.DataAccess.Mappings
{
    public class WorkItemMap : ClassMap<WorkItem>
    {
        public WorkItemMap()
        {
            Table("work_item");

            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Description);
        }
    }
}
