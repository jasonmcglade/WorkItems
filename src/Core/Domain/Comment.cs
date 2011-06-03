using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public class Comment
    {
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
        public string User { get; set; }
    }
}
