using System;

namespace Col7
{
    class Program
    {
        //решение по алгоритму Бойера-Мура
        //поиск пары с различающимся элементом для каждого индекса
        //оставшиеся без пары элементы будут искомыми
        static void Main(string[] args)
        {
            Console.WriteLine("Введите массив");
            var array = Console.ReadLine().Split(' ');
            //рассматриваемый элемент и их количество
            string element = "";
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                //если кол-во элементов равно 0, то рассматриваем новый элемент
                if (count == 0)
                {
                    element = array[i];
                    count++;
                }
                //если элементы совпадают, то увеличиваем их количество
                else if (element == array[i]) count++;
                //если нашли пару, то убираем один элемент
                else count--;                  
            }
            //выводим элемент оставшихся без пары индексов
            Console.WriteLine("Искомый элемент равен {0}", element);
            Console.ReadKey();
        }
    }
}
