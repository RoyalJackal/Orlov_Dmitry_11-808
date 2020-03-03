using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmptyWeb.Models;
using EmptyWeb.Services;
using Microsoft.AspNetCore.Http;

namespace EmptyWeb.Controllers
{
    public class CommentController
    {
        private readonly IStorage _storage;

        public CommentController(IStorage storage)
        {
            _storage = storage;
        }
        
        public async Task GetCommentForm(HttpContext context)
        {
	        var parentName = context.Request.Path.Value.Split('/').LastOrDefault();
	        await context.Response.WriteAsync($@"
				<!DOCTYPE html>
				<html>
				<head>
				<meta charset='utf-8' />
				<title></title>
				</head>
				<body>
				<form action='/Comment/{parentName}' method='post'  enctype='multipart/form-data'> 
				Username: <input name='username' /> <br />
				Message Name: <input name='messagename' /> <br />
	            Message Text: <textarea name='text'></textarea> <br />
	            Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
				</form>
				</body>
				</html>");
        }

        public async Task CreateComment(HttpContext context)
        {
	        var parentName = context.Request.Path.Value.Split('/').LastOrDefault();
	        var folderName = parentName.Remove(parentName.Length - 4, 4) + "_comments";
	        
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
			
			var comment = new Comment(
				context.Request.Form["username"],
				context.Request.Form["messagename"],
				context.Request.Form["text"],
				time,
				imageString,
				folderName);
			
			var valResults = Validation.Validation.Validate(comment);
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
				<form action='/Comment/{parentName}' method='post'  enctype='multipart/form-data'> 
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
			
			_storage.Save(comment);

			await context.Response.WriteAsync(ReturnPageMaker.Make("New Comment was added"));
        }
        
        public async Task GetEditForm(HttpContext context)
        {
	        var parts = context.Request.Path.Value.Split('/');
			var name = parts.LastOrDefault();
			var parent = parts[^2];
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == parent);
			var comment = _storage
				.GetComments(message)
				.FirstOrDefault(c => c.GetCsvName() == name);
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
				<form action='/EditComment/{parent}/{name}' method='post'  enctype='multipart/form-data'> 
				Username: {comment.Username} <br />
				Message Name: <input name='messagename' value='{comment.MessageName}' /> <br />
				Message Text: <textarea name='text'>{comment.Text}</textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
				</form>
				</body>
				</html>");
		}

		public async Task EditComment(HttpContext context)
		{
			var parts = context.Request.Path.Value.Split('/');
			var name = parts.LastOrDefault();
			var parent = parts[^2];
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == parent);
			var comment = _storage
				.GetComments(message)
				.FirstOrDefault(c => c.GetCsvName() == name);
			Comment newComment;
			
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

				newComment = new Comment(comment.Username,
					context.Request.Form["messagename"],
					context.Request.Form["text"],
					comment.Date,
					imageString,
					parent.Remove(parent.Length - 4, 4) + "_comments");
			}
			else
				newComment = new Comment(comment.Username,
					context.Request.Form["messagename"],
					context.Request.Form["text"],
					comment.Date,
					comment.Image,
					parent.Remove(parent.Length - 4, 4) + "_comments");
			
			var valResults = Validation.Validation.Validate(newComment);
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
				<form action='/EditComment/{parent}/{name}' method='post'  enctype='multipart/form-data'> 
				Username: {comment.Username} <br />
				{valResults[2].ErrMsg} <br />
				Message Name: <input name='messagename' value='{comment.MessageName}' /> <br />
				Message Text: <textarea name='text'>{comment.Text}</textarea> <br />
				Image: <input name='image' type='file' accept='image/*'/> <br />
				<input type='submit' value='Post'/> <br />
				</form>
				</body>
				</html>");
				return;
			}
			
			_storage.Save(newComment);
			await context.Response.WriteAsync(ReturnPageMaker.Make("Comment was edited"));
		}

		public async Task DeleteComment(HttpContext context)
		{
			var parts = context.Request.Path.Value.Split('/');
			var name = parts.LastOrDefault();
			var parent = parts[^2];
			var message = _storage
				.LoadAll()
				.FirstOrDefault(m => m.GetCsvName() == parent);
			var comment = _storage
				.GetComments(message)
				.FirstOrDefault(c => c.GetCsvName() == name);
			_storage.Delete(comment);
			await context.Response.WriteAsync(ReturnPageMaker.Make("Comment was deleted"));
		}
    }
}