using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public class WorkItem
    {
        private IList<Comment> _comments;

        public WorkItem()
        {
            _comments = new List<Comment>();
        }

        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Staus { get; set; }

        public virtual IEnumerable<Comment> Comments 
        { 
            get { return _comments; }
        }

        public virtual void AddComment(string text, string user)
        {
            _comments.Add(new Comment(text, user) { WorkItem = this});
        }


    }
}
