using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr12
{
    class Program
    {
        static void Main(string[] args)
        {
            //разделение строчки и распределение переменных
            String s = Console.ReadLine();
            String[] parts = s.Split(' ');
            int h = int.Parse(parts[0]);
            int t = int.Parse(parts[1]);
            int v = int.Parse(parts[2]);
            int x = int.Parse(parts[3]);
            double min;
            double max;
            //вычесление минимального времени
            if (h - t * x <= 0)
            { min = 0; }
            else { min = ((h - t * x) / (v - x)); };
            //вычесление максимального времени
            if (h / t > x)
            { max = t; }
            else { max = (h - t) / x; };
            Console.WriteLine("Минимальное время равно {0} Максимальное время равно {1}", min, max);
            Console.ReadKey();
        }
    }
}
