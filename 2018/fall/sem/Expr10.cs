using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr10
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            //перебор всех значений
            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0  || i % 5 == 0)
                { sum += i; };
            }
            Console.WriteLine("Сумма всех положительных чисел меньше 1000 кратных 3 или 5 равна " + sum);
            Console.ReadKey();
        }
    }
}
