using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContrLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task1
            /*
            foreach (var item in Task1.Solve(new string[] { "ABC", "", "AAA", "CCC" }))
                Console.WriteLine(item);
            Console.ReadKey();
            */

            //Task2
            /*
            foreach (var item in new int[] { 1, 2, 3 }.Solve<int>(new int[] { 4, 5, 6 },
                new Func<int, int, bool>((i1, i2) => (i1 + i2) % 2 == 0)))
                Console.WriteLine(item);
            Console.ReadKey();
            */

            //Task3
            var products = new Product[]
            {
                new Product("a", "a", "Kazakhstan"),
                new Product("b", "b", "Kazakhstan")
            };
            var discounts = new Discount[]
            {
                new Discount(1, "pyatyorochka", 0),
                new Discount(2, "pyatyorochka", 50),
                new Discount(1, "eldorado", 50)
            };
            var prices = new Price[]
            {
                new Price("a", "pyatyorochka", 100),
                new Price("b", "eldorado", 200)
            };
            var purchases = new PurchaseData[]
            {
                new PurchaseData(1, "a", "pyatyorochka"),
                new PurchaseData(1, "b", "eldorado"),
                new PurchaseData(2, "a", "pyatyorochka")
            };
            foreach (var item in Task3.Solve(products, discounts, prices, purchases))
                Console.WriteLine(item);
            Console.ReadKey();
        }
    }
}
