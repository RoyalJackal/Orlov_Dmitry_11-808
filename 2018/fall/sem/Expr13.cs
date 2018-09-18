using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr13
{
    class Program
    {
        static void Main(string[] args)
        {
            //разделение строчки и распределение переменных
            String s = Console.ReadLine();
            String[] parts = s.Split(' ');
            int side = int.Parse(parts[0]);
            int radius = int.Parse(parts[1]);
            //круг находится в квадрате
            if (radius <= side / 2)
            { Console.WriteLine(Math.PI * radius * radius); };
            //квадрат находится в круге
            if (radius >= Math.Sqrt(2) * side / 2)
            { Console.WriteLine(side * side); };
            //круг пересекает квадрат
            if (side / 2 < radius && radius < Math.Sqrt(2) * side / 2)
            { Console.WriteLine(Math.Round(Math.PI * radius * radius - 4 * (SectorArea(radius, side) - TriangleArea(radius, side)),3)); };
            Console.ReadKey();
        }
        static double SectorArea(int r, int a)
        {
            return (180 * r * r * 2 * Math.Acos((double)a / (2 * r))) / 360;
        }
        static double TriangleArea(int r, int a)
        {
            return Math.Sqrt(r * r - (a * a) / 4) * a / 2;
        }
    }
}
