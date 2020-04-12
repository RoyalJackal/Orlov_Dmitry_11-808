using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSn.Service
{
    public class CommentViewModel
    {
        public CommentViewModel(UserViewModel sender, PostViewModel post, string name, string text, DateTime date)
        {
            Sender = sender;
            Post = post;
            Name = name;
            Text = text;
            Date = date;
        }

        public UserViewModel Sender { get; set; }
        public PostViewModel Post { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
