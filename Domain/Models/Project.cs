using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime? Deadline { get; set; }

        public ProjectStates State { get; set; } = ProjectStates.Inbox;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<ActionItem> ActionItems { get; set; } = new List<ActionItem>();

        public string Source { get; set; }

        public ProjectPriorities Priority { get; set; } = ProjectPriorities.Normal;
    }
}