using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EmptyWeb.Models;
using System.Text;
using System.Drawing;

namespace EmptyWeb
{
	public class HomeController
	{
		private IStorage storage;

		public HomeController(IStorage storage)
		{
			this.storage = storage;
		}

		public async Task GetForm(HttpContext context)
		{
			await context.Response.WriteAsync(File.ReadAllText("Views\\index.html"));
		}

		public async Task AddEntry(HttpContext context)
		{
			string filePath = "Files";


			DateTime time = DateTime.Now;
			string userName = context.Request.Form["username"];
			string messageName = context.Request.Form["messagename"];
			string text = context.Request.Form["text"];
			string imageString = "";

			var formFile = context.Request.Form.Files[0];
			if (formFile.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					formFile.CopyTo(ms);
					var fileBytes = ms.ToArray();
					imageString = Convert.ToBase64String(fileBytes);
				}
			}

			string csvFileName = Path.Combine(filePath, userName + "_" + time.Day + "_" + time.Month + "_" + time.Year + "_" + time.Minute + "_" + time.Hour + "_" + time.Second + ".csv");
			var builder = new StringBuilder();			
			builder.Append(userName);
			builder.Append(",");
			builder.Append(messageName);
			builder.Append(",");
			builder.Append(text);
			builder.Append(",");
			builder.Append(time.ToString());
			builder.Append(",");
			builder.Append(imageString);
			File.AppendAllLines(csvFileName, new string[] { builder.ToString() });

			await context.Response.WriteAsync("New Entry was added");
		}

		public async Task GetPosts(HttpContext context)
		{
			string filePath = "Files";

			string[] files = Directory.GetFiles(filePath, "*.csv", SearchOption.AllDirectories);
			var messages = new List<Message>();

			foreach (var file in files)
			{
				var parts = File.ReadAllText(file).Split(',');
				var message = new Message(parts[0], parts[1], parts[2], parts[3], parts[4]);
				messages.Add(message);
			}

			var builder = new StringBuilder();
			builder.Append(@"
<!DOCTYPE html>
<html>
<head>
	<meta charset = 'utf-8'/>
	<title> Cписок постов </title>
</head>
<body>");
			foreach (var message in messages)
			{
				builder.Append($@"
<div class='container'>
		Username: {message.Username} <br/>
		Message Name: {message.MessageName} <br/>
		Message Text: {message.Text} <br/>
		Sending Date: {message.Date} <br/>
		Image: <img src='data: image / png; base64,{message.Image}'> <br/>
</div>
");
			}
			builder.Append(@"
</body>
</html>");

			await context.Response.WriteAsync(builder.ToString());
		}
	}
}
