using System.Collections.Generic;
using EmptyWeb.Models;

namespace EmptyWeb.Services
{
	public interface IStorage
	{
		void Save(Message message);
		List<Message> LoadAll();
		void Delete(Message message);
		void Delete(Comment comment);
		void Delete(string path);
	}
}
