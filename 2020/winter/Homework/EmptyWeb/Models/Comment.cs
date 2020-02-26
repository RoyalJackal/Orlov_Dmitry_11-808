using System;

namespace EmptyWeb.Models
{
    public class Comment
    {
        public string Username { get; set; }
        public string MessageName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public Message Parent { get; set; }

        public Comment(string username, string messagename, string text, DateTime date, string image, Message parent)
        {
            Username = username;
            MessageName = messagename;
            Text = text;
            Date = date;
            Image = image;
            Parent = parent;
        }

        public Comment(String scv)
        {
            
        }
    }
}