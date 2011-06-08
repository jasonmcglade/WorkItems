using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public class Comment : Entity
    {
        protected Comment()
        {
        }

        public Comment(string text, string user)
        {
            Text = text;
            User = user;
            AddedDate = DateTime.UtcNow;
        }

        public virtual WorkItem WorkItem { get; set; }
        public virtual string Text { get; private set; }
        public virtual DateTime AddedDate { get; private set; }
        public virtual string User { get; private set; }
    }
}
