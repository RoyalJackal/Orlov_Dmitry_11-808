using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyWeb.Models;
using EmptyWeb.Services;
using Microsoft.AspNetCore.Http;
using EmptyWeb.Validation;

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

			var valResults = Validation.Validation.Validate(message);
			if (valResults.Any(result => !result.IsValid))
			{
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
				<form action='/Home/AddEntry/' method='post'  enctype='multipart/form-data'> 
				{valResults[0].ErrMsg} <br />
				Username: <input name=username /> <br />
				{valResults[1].ErrMsg} <br />
				Message Name: <input name='messagename' /> <br />
				Message Text: <textarea name='text'></textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
				</form>
				</body>
				</html>");
				return;
			}
			_storage.Save(message);

			await context.Response.WriteAsync(ReturnPageMaker.Make("New Entry was added"));
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
	<title> Post List </title>
</head>
<body>
	<form action='/'>
		<input type='submit' value='Return' />
	</form>");

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
		<form action='/Comment/{message.GetCsvName()}'>
			<input type='submit' value='Comment' />
		</form>
	</div>
");
				foreach (var comment in _storage.GetComments(message))
				{
					builder.Append($@"
	<div class='container' style ='margin-left:40px'>
		Username: {comment.Username} <br/>
		Message Name: {comment.MessageName} <br/>
		Message Text: {comment.Text} <br/>
		Sending Date: {comment.Date.ToString(CultureInfo.InvariantCulture)} <br/>
		Image: <img src='data: image / png; base64,{comment.Image}'> <br/>
		<form action='/EditComment/{message.GetCsvName()}/{comment.GetCsvName()}'>
			<input type='submit' value='Edit' />
		</form>
		<form action='/DeleteComment/{message.GetCsvName()}/{comment.GetCsvName()}'>
			<input type='submit' value='Delete' />
		</form>
	</div>
");
				}

				builder.Append("<br/>");
			}
			
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
				<form action='/Edit/{name}' method='post'  enctype='multipart/form-data'> 
				Username: {message.Username} <br />
				Message Name: <input name='messagename' value='{message.MessageName}' /> <br />
				Message Text: <textarea name='text'>{message.Text}</textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
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

			Message newMessage;

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

				newMessage = new Message(message.Username,
					context.Request.Form["messagename"],
					context.Request.Form["text"],
					message.Date,
					imageString);
			}
			else
				newMessage = new Message(message.Username, context.Request.Form["messagename"], context.Request.Form["text"], message.Date, message.Image);
			
			var valResults = Validation.Validation.Validate(newMessage);
			if (valResults.Any(result => !result.IsValid))
			{
				await context.Response.WriteAsync($@"
				<!DOCTYPE html>
				<html>
				<head>
				<meta charset='utf-8' />
				<title></title>
				</head>
				<body>
				<form action='/Edit/{name}' method='post'  enctype='multipart/form-data'> 
				Username: {message.Username} <br />
				{valResults[1].ErrMsg} <br />
				Message Name: <input name='messagename' value='{message.MessageName}' /> <br />
				Message Text: <textarea name='text'>{message.Text}</textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
				</form>
				</body>
				</html>");
				return;
			}
			
			_storage.Save(newMessage);
			await context.Response.WriteAsync(ReturnPageMaker.Make("Entry was edited"));
		}

		public async Task DeleteMessage(HttpContext context)
		{
			var name = context.Request.Path.Value.Split('/').LastOrDefault();
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == name);
			_storage.Delete(message);
			await context.Response.WriteAsync(ReturnPageMaker.Make("Entry was deleted"));
		}
	}
}
