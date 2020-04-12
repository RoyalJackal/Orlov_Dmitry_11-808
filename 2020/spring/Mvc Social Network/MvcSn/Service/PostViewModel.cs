using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSn.Service
{
    public class PostViewModel
    {
        public PostViewModel(UserViewModel sender, string name, string text, DateTime date, List<CommentViewModel> comments)
        {
            Sender = sender;
            Name = name;
            Text = text;
            Date = date;
            Comments = comments;
        }

        public UserViewModel Sender { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
