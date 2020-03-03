using System.Collections.Generic;
using System.Globalization;
using System.IO;
using EmptyWeb.Models;

namespace EmptyWeb.Services
{
	public class MessageStorage : IStorage
	{
		private const string FilePath = "Files";

		public void Save(Message message)
		{
			var csvFileName = Path.Combine(FilePath, message.GetCsvName());
			File.WriteAllText(csvFileName, Csv.Make(
					message.Username,
					message.MessageName,
					message.Text, 
					message.Date.ToString(CultureInfo.InvariantCulture), 
					message.Image));
			Directory.CreateDirectory(Path.Combine(FilePath, message.GetCsvName().Remove(message.GetCsvName().Length - 4, 4) + "_comments"));
		}
		
		public void Save(Comment comment)
		{
			var csvFileName = Path.Combine(FilePath, comment.Folder, comment.GetCsvName());
			File.WriteAllText(csvFileName, Csv.Make(
				comment.Username,
				comment.MessageName,
				comment.Text, 
				comment.Date.ToString(CultureInfo.InvariantCulture), 
				comment.Image));
		}

		public List<Message> LoadAll()
		{
			var result = new List<Message>();
			foreach (var file in Directory.GetFiles(FilePath, "*.csv"))
			{
				var message = new Message(File.ReadAllText(file));
				result.Add(message);
			}
			return result;
		}

		public List<Comment> GetComments(Message message)
		{
			var result = new List<Comment>();
			var path = Path.Combine(FilePath, message.GetCsvName().Remove(message.GetCsvName().Length - 4, 4) + "_comments");
			var folder = new DirectoryInfo(path);
			foreach (var file in folder.GetFiles())
				result.Add(new Comment(File.ReadAllText(file.FullName), folder.Name));
			return result;
		}

		public void Delete(Message message)
		{
			File.Delete(Path.Combine(FilePath, message.GetCsvName()));
		}

		public void Delete(Comment comment)
		{
			File.Delete(Path.Combine(FilePath, comment.Folder, comment.GetCsvName()));
		}

		public void Delete(string path)
		{
			File.Delete(Path.Combine(FilePath, path));
			var commentDirectoryName = path.Remove(path.Length - 4, 4) + "_comments";
			if (!Directory.Exists(commentDirectoryName)) return;
			var commentDirectory = new DirectoryInfo(commentDirectoryName);
			foreach (var file in commentDirectory.GetFiles())
				file.Delete();
			commentDirectory.Delete();
		}
	}
}
