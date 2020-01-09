using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Npgsql;

namespace Exam.Models
{
    public static class Scaner
    {
        public static List<Tuple<string, string>> ScanPage(string pageUrl, string startUrl, int depth, int currDepth)
        {
            try
            {
                var pageUri = new Uri(pageUrl);
                var domain = pageUri.Host.Replace("www.", string.Empty);
                var urls = new List<string>();
                var web = new HtmlWeb();
                var doc = web.Load(pageUrl);

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = link.Attributes["href"];
                    if (att.Value.Contains(domain))
                        urls.Add(att.Value);
                }

                var innerText = doc.DocumentNode.InnerText.Substring(0, 500);
                using (var connection = new NpgsqlConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"INSERT INTO url VALUES ('{startUrl}', '{pageUrl}');", connection);
                    command.ExecuteNonQuery();                      
                    connection.Close();
                }
                    

                var urlAndData = new List<Tuple<string, string>>();
                urlAndData.Add(new Tuple<string, string>(pageUrl, innerText));
                if (currDepth < depth)
                    foreach (var url in urls)
                        urlAndData.AddRange(ScanPage(url, startUrl, depth, currDepth + 1));
                return urlAndData;
            }
            catch
            {
                return new List<Tuple<string, string>>();
            }         
        }

        public static List<Tuple<string, string>> GetDataByUrl(string url)
        {
            var urls = new List<UrlModel>();
            using (var connection = new NpgsqlConnection(Connection.ConnectionString))
            {
                connection.Open();
                using (var cmd =
                new NpgsqlCommand($"select * from url where base_url = '{url}';", connection))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var currUrls = new UrlModel();
                            currUrls.BaseUrl = reader.GetString(0);
                            currUrls.PageUrl = reader.GetString(1);
                            urls.Add(currUrls);
                        }
                    }
                }
                connection.Close();
            }

            var results = new List<Tuple<string, string>>();
            if (urls.Count > 0)
            {
                foreach (var currUrl in urls)
                {
                    var web = new HtmlWeb();
                    var doc = web.Load(currUrl.PageUrl);
                    var innerText = doc.DocumentNode.InnerText.Substring(0, 500);
                    results.Add(new Tuple<string, string>(currUrl.PageUrl, innerText));
                }
            }

            return results;
        }
    }
}
