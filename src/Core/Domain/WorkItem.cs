using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public class WorkItem : Entity
    {

        public WorkItem()
        {
            Comments = new List<Comment>();
        }

        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Staus { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string User { get; set; }

        public virtual IList<Comment> Comments {get; set; }

        public virtual void AddComment(string text, string user)
        {
            Comments.Add(new Comment(text, user) { WorkItem = this });
        }


    }
}
