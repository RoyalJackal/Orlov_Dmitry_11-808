using System;
using System.Globalization;
using System.Text;
using EmptyWeb.Validation;

namespace EmptyWeb.Models
{
    public class Comment
    {
        [NotEmpty]
        [ValidUsername]
        public string Username { get; set; }
        [NotEmpty]
        public string MessageName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string Folder { get; set; }

        public Comment(string username, string messagename, string text, DateTime date, string image, string folder)
        {
            Username = username;
            MessageName = messagename;
            Text = text;
            Date = date;
            Image = image;
            Folder = folder;
        }
        
        public Comment(string csv, string folder)
        {
            var parts = csv.Split(',');
            Username = parts[0];
            MessageName = parts[1];
            Text = parts[2];
            Date = Convert.ToDateTime(parts[3], CultureInfo.InvariantCulture);
            Image = parts[4];
            Folder = folder;
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