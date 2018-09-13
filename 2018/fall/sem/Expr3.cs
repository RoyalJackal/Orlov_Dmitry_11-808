using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите время в часах");
            int hour = Convert.ToInt16(Console.ReadLine());
            int deg = 180 - Math.Abs(hour % 12 - 6) * 30;
            Console.WriteLine("Угол между стрелками равен " + deg);
            Console.ReadKey();
        }
    }
}
