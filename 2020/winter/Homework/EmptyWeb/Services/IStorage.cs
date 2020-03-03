using System.Collections.Generic;
using EmptyWeb.Models;

namespace EmptyWeb.Services
{
	public interface IStorage
	{
		void Save(Message message);
		void Save(Comment comment);
		List<Message> LoadAll();
		List<Comment> GetComments(Message message);
		void Delete(Message message);
		void Delete(Comment comment);
		void Delete(string path);
	}
}
