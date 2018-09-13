using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr8
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод данных
            Console.WriteLine("Введите координату x точки");
            double dotx = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите координату y точки");
            double doty = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите коэффициент a прямой из формулы y = ax + b");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите коэффициент b прямой из формулы y = ax + b");
            double b = Convert.ToDouble(Console.ReadLine());
            //вычесление свободного члена второй пямой
            double b2 = doty + dotx / a;
            //нахождение координат точки пересечения
            double x = ((b2 - b) * a) / (a * a + 1);
            double y = a * x + b;
            Console.WriteLine("x = {0}, y = {1}", x, y);
            Console.ReadKey();
        }
    }
}
