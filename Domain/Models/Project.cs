using System;
using System.Collections.Generic;

namespace TaskMaster.Domain.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? Deadline { get; set; }

        public ProjectState State { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<ActionItem> ActionItems { get; set; }
    }
}