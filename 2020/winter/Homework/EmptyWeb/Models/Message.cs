using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyWeb.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string MessageName { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }

        public Message(string username, string messagename, string text, string date, string image)
        {
            Username = username;
            MessageName = messagename;
            Text = text;
            Date = date;
            Image = image;
        }
    }
}
