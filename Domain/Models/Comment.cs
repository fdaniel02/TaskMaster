using System;

namespace TaskMaster.Domain.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }
    }
}