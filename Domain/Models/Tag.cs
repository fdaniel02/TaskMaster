using System.Collections.Generic;

namespace Domain.Models
{
    public class Tag
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<ProjectTags> ProjectTags { get; set; }
    }
}