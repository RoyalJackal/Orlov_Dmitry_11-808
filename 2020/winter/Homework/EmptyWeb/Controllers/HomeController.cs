using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyWeb.Models;
using EmptyWeb.Services;
using Microsoft.AspNetCore.Http;

namespace EmptyWeb.Controllers
{
	public class HomeController
	{
		
		private readonly IStorage _storage;

		public HomeController(IStorage storage)
		{
			_storage = storage;
		}

		public async Task GetForm(HttpContext context)
		{
			await context.Response.WriteAsync(File.ReadAllText("Views\\index.html"));
		}

		public async Task AddEntry(HttpContext context)
		{
			var time = DateTime.Now;
			var imageString = "";

			var formFiles = context.Request.Form.Files;
			if (formFiles.Count != 0)
				if (formFiles[0].Length > 0)
				{
					await using var ms = new MemoryStream();
					formFiles[0].CopyTo(ms);
					var fileBytes = ms.ToArray();
					imageString = Convert.ToBase64String(fileBytes);
				}
			
			var message = new Message(
				context.Request.Form["username"],
				context.Request.Form["messagename"],
				context.Request.Form["text"],
				time,
				imageString);
			_storage.Save(message);

			await context.Response.WriteAsync("New Entry was added");
		}

		public async Task GetPosts(HttpContext context)
		{
			var messages = _storage.LoadAll();
			
			var builder = new StringBuilder();
			builder.Append(@"
<!DOCTYPE html>
<html>
<head>
	<meta charset = 'utf-8'/>
	<title> Cписок постов </title>
</head>
<body>");

			foreach (var message in messages) {
				builder.Append($@"
<div class='container'>
		Username: {message.Username} <br/>
		Message Name: {message.MessageName} <br/>
		Message Text: {message.Text} <br/>
		Sending Date: {message.Date.ToString(CultureInfo.InvariantCulture)} <br/>
		Image: <img src='data: image / png; base64,{message.Image}'> <br/>
		<form action='/Edit/{message.GetCsvName()}'>
			<input type='submit' value='Edit' />
		</form>
		<form action='/Delete/{message.GetCsvName()}'>
			<input type='submit' value='Delete' />
		</form>
</div>
"); }
			
			builder.Append(@"

</body>
</html>");

			await context.Response.WriteAsync(builder.ToString());
		}
		
		public async Task GetEditForm(HttpContext context)
		{
			var name = context.Request.Path.Value.Split('/').LastOrDefault();
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == name);
			await context.Response.WriteAsync($@"
				<!DOCTYPE html>
				<html>
				<head>
				<meta charset='utf-8' />
				<title></title>
				</head>
				<body>
				<form action='/PostsList/'>
				<input type='submit' value='View messages' />
				</form>
				<form action='/Edit/{name}' method='post'  enctype='multipart/form-data'> 
				Username: {message.Username} <br />
				Message Name: <input name='messagename' value='{message.MessageName}' /> <br />
				Message Text: <textarea name='text'>{message.Text}</textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit'/> <br />
				</form>
				</body>
				</html>");
		}

		public async Task EditMessage(HttpContext context)
		{
			var name = context.Request.Path.Value.Split('/').LastOrDefault();
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == name);
			var files = context.Request.Form.Files;
			if (files.Count > 0)
			{
				var imageString = "";
				if (files[0].Length > 0)
				{
					await using var ms = new MemoryStream();
					files[0].CopyTo(ms);
					var fileBytes = ms.ToArray();
					imageString = Convert.ToBase64String(fileBytes);
				}

				_storage.Save(new Message(message.Username, 
					context.Request.Form["messagename"], 
					context.Request.Form["text"], 
					message.Date,
					imageString));
				await context.Response.WriteAsync("Entry was edited");
			}
			_storage.Save(new Message(message.Username, context.Request.Form["messagename"], context.Request.Form["text"], message.Date, message.Image));
			await context.Response.WriteAsync("Entry was edited");
		}

		public async Task DeleteMessage(HttpContext context)
		{
			var name = context.Request.Path.Value.Split('/').LastOrDefault();
			_storage.Delete(name);
			await context.Response.WriteAsync("Entry was deleted");
		}
	}
}
