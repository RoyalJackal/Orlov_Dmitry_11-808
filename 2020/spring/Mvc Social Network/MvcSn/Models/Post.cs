﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSn.Models
{
    public class Post
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public string SenderName { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
