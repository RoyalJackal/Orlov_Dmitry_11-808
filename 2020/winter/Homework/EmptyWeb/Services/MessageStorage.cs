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

		public void Delete(Message message)
		{
			File.Delete(Path.Combine(FilePath, message.GetCsvName()));
		}

		public void Delete(Comment comment)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(string path)
		{
			File.Delete(Path.Combine(FilePath, path));
		}
	}
}
