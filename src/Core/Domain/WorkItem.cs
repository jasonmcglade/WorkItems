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

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Staus { get; set; }

        public IEnumerable<Comment> Comments 
        { 
            get { return _comments; }
        }


    }
}
