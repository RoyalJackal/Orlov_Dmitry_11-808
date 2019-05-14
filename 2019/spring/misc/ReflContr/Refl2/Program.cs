using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Refl2
{
    class Program
    {
        const string url = @"https://jsonplaceholder.typicode.com/comments";

        static void Main(string[] args)
        {
            //считываем со ссылки текст
            var request = WebRequest.Create(url);
            var responce = request.GetResponse();
            var reader = new StreamReader(responce.GetResponseStream());

            //десереализируем текст и параллельно проходим по каждому комментарию
            var input = reader.ReadToEnd();
            var decerializedInput = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(input);
            Parallel.ForEach(decerializedInput, WriteLetterCount);
            Console.ReadKey();
        }

        //вычисление количества символов в комментарии
        public static void WriteLetterCount(Newtonsoft.Json.Linq.JToken comment)
        {
            //если id нечётный, то пропускаем, иначе выводим id и количество букв для него
            if ((int)comment["id"] % 2 == 1) return;
            Console.WriteLine("Id = {0}; Letter count = {1}", comment["id"], comment["body"].ToString().Length);
        }
    }
}
