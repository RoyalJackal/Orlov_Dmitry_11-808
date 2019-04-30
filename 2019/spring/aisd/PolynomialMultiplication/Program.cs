using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PolynomialMultiplication
{
    //При трансформации векторов может получиться так, что некоторые элементы
    //будут иметь длинную дробную часть с множеством девяток. Это происходит из-за
    //погрешности в операциях вычисления и такие элементы следует округлять.
    class Program
    {
        static void Main(string[] args)
        {
            var polinomial1 = new Complex[] 
            {
                new Complex(-2, 0),
                new Complex(1, 0),
                new Complex(1, 0)
            };
            var polinomial2 = new Complex[]
            {
                new Complex(-2, 0),
                new Complex(1, 0),
                new Complex(1, 0)
            };
            var multiplication = FourierTransform.MultiplyPolinomials(polinomial1, polinomial2);
            Console.WriteLine("Итоговый вектор:");
            foreach (var coeff in multiplication)
                Console.Write(coeff + " ");
            Console.ReadKey();
        }
    }
}
