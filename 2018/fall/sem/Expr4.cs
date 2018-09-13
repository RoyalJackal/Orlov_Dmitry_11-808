using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr4
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод данных
            Console.WriteLine("Введите число N");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите число X");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите число Y");
            int y = Convert.ToInt32(Console.ReadLine());
            //основая формула и вывод
            Console.WriteLine(n /x + n/y - n/(x+y));
            Console.ReadKey();
        }
    }
}
