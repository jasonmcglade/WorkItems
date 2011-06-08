using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using WorkItems.Core.Domain;

namespace WorkItems.Core.DataAccess.Mappings
{
    public class WorkItemMap : EntityMap<WorkItem>
    {
        public WorkItemMap()
        {
            Table("work_item");

            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.CreatedDate, "created_date");
            Map(x => x.User);

            HasMany<Comment>(x => x.Comments).KeyColumn("work_item_id").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
