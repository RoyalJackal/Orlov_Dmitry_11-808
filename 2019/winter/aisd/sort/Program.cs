using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            //создаём массив слов и определяем длину наибольшего из них
            Console.WriteLine("Введите слова через пробел");
            var words = Console.ReadLine().Split(' ');
            int length = 0;
            for (int i = 0; i < words.Count(); i++)
                if (length < words[i].Length)
                    length = words[i].Length;
            //сортируем массив слов по каждому символу начиная с последнего
            for (int i = length - 1; i >= 0; i--)
            {
                words = SortBySymbol(i, words);
            }
            foreach (var item in words)
                Console.WriteLine(item);
            Console.ReadKey();
        }

        //класс сортировки массива по сивмолу на одном разряде
        public static string[] SortBySymbol(int symbolPos, string[] words)
        {
            //создаём и инициализируем массив очередей
            var sorter = new Queue<string>[27];
            for (int i = 0; i < sorter.Length; i++)
                sorter[i] = new Queue<string>();
            foreach (var word in words)
            {
                //если слово короче позиции нынешнего слова, ставим его в первую очередь
                if (word.Length <= symbolPos)
                    sorter[0].Push(word);
                //иначе ставим его в очередь по индексом номера символа в алфавите
                else
                    sorter[Char.ToLower(word[symbolPos]) - 'a' + 1].Push(word);
            }
            int symbolIndex = 0;
            //поочерёдно выписываем слова из очередей в массив и выводим полученный
            for (int i = 0; i < words.Length; i++)
            {
                while (sorter[symbolIndex].IsEmpty())
                    symbolIndex++;
                words[i] = sorter[symbolIndex].Pop();
            }
            return words;
        }
    }
}
