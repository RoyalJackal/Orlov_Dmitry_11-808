using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cond2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] parts = Console.ReadLine().Split(' ');
            int x = Int32.Parse(parts[0]);
            int y = Int32.Parse(parts[1]);
            int z = Int32.Parse(parts[2]);
            int a = Int32.Parse(parts[3]);
            int b = Int32.Parse(parts[4]);
            int min = Math.Min(Math.Min(x, y), z);
            if (min > Math.Min(a, b)) Console.WriteLine("Не пролезет");
            else
                if (min == x)
                    if (Math.Min(y, z) > Math.Max(a, b)) Console.WriteLine("Не пролезет");
                    else Console.WriteLine("Пролезет");
                else
                    if (min == y)
                        if (Math.Min(x, z) > Math.Max(a, b)) Console.WriteLine("Не пролезет");
                        else Console.WriteLine("Пролезет");
                    else
                        if (Math.Min(x, y) > Math.Max(a, b)) Console.WriteLine("Не пролезет");
                        else Console.WriteLine("Пролезет");
            Console.ReadKey();
        }
    }
}
