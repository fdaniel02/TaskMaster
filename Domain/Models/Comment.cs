using System;

namespace Domain.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }
    }
}