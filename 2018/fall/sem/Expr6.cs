using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr6
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод координат
            Console.WriteLine("Введите координату x точки");
            double xa = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату y точки");
            double ya = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату x первой точки прямой");
            double xb = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату y первой точки прямой");
            double yb = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату x второй точки прямой");
            double xc = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите координату y второй точки прямой");
            double yc = Convert.ToInt32(Console.ReadLine());
            //нахождение сторон треугольника
            double ab = Math.Sqrt((xa - xb) * (xa - xb) + (ya - yb) * (ya - yb));
            double bc = Math.Sqrt((xb - xc) * (xb - xc) + (yb - yc) * (yb - yc));
            double ac = Math.Sqrt((xa - xc) * (xa - xc) + (ya - yc) * (ya - yc));
            //нахождение полупериметра
            double p = (ab + bc + ac) / 2;
            //нахождение площади
            double S = Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac));
            Console.WriteLine("Расстояние между точкой и прямой равно " + (2 * S) / 2);
            Console.ReadKey();
        }
    }
}