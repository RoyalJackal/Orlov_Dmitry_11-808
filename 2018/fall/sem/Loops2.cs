using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Int32.Parse(Console.ReadLine());
            int counter = 0;
            for (int i = 1; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (n - i - j >= 0 && n - i - j < 10) counter++; 
                }
            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
