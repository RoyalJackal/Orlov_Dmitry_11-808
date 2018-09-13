using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr5
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод данных
            Console.WriteLine("Введите левую границу");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите правую границу");
            int b = Convert.ToInt32(Console.ReadLine());
            //основая формула и вывод
            if (a%4 != 0)
            { Console.WriteLine((b / 4) - (a / 4) - (b / 100) + (a / 100) + (b / 400) - (a / 400)); }
            else
            //при левой границе кратной 4 прибавляется единица
            { Console.WriteLine((b / 4) - (a / 4) - (b / 100) + (a / 100) + (b / 400) - (a / 400) + 1); }
            Console.ReadKey();
        }
    }
}
