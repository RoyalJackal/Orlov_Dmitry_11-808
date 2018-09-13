using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите координату a из формулы y = ax + b");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату b из формулы y = ax + b");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Параллельный вектор: (1;" + a + ")");
            Console.WriteLine("Перпендикулярный вектор: (1;-1/" + a + ")");
            Console.ReadKey();
        }
    }
}
