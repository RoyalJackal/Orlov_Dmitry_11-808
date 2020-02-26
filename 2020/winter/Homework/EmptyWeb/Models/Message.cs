using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWeb.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string MessageName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public Comment[] Comments { get; set; }

        public Message(string username, string messagename, string text, DateTime date, string image)
        {
            Username = username;
            MessageName = messagename;
            Text = text;
            Date = date;
            Image = image;
        }

        public Message(string csv)
        {
            var parts = csv.Split(',');
            Username = parts[0];
            MessageName = parts[1];
            Text = parts[2];
            Date = Convert.ToDateTime(parts[3], CultureInfo.InvariantCulture);
            Image = parts[4];
        }

        public string GetCsvName()
        {
            var builder = new StringBuilder();
            builder.Append(Username);
            builder.Append("_");
            builder.Append(Date.Day);
            builder.Append("_");
            builder.Append(Date.Month);
            builder.Append("_");
            builder.Append(Date.Year);
            builder.Append("_");
            builder.Append(Date.Minute);
            builder.Append("_");
            builder.Append(Date.Hour);
            builder.Append("_");
            builder.Append(Date.Second);
            builder.Append(".csv");
            return builder.ToString();
        }
    }
}
