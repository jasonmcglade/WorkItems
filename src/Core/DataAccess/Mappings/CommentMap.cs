using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItems.Core.Domain;

namespace WorkItems.Core.DataAccess.Mappings
{
    public class CommentMap : EntityMap<Comment>
    {
        public CommentMap()
        {
            Table("comment");

            Map(x => x.Text);
            Map(x => x.AddedDate, "added_date");
            Map(x => x.User);
            References<WorkItem>(x => x.WorkItem).Column("work_item_id").Cascade.All();
        }

    }
}
