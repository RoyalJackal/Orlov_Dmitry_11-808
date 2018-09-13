using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число");
            //ввод числа
            int num =Convert.ToInt32(Console.ReadLine());
            int revnum=0;
            //основной цикл
            for (int i=0; i < 3; i++)
            {
                revnum = revnum * 10 + num % 10;
                num = num / 10;
            }
            Console.WriteLine("Перевёрнутое число: " + revnum);
            Console.ReadKey();
        }
    }
}
