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
            Console.WriteLine("Введите первый вектор");
            var polinomial1 = Reader.ReadVector();
            Console.WriteLine("Введите второй вектор");
            var polinomial2 = Reader.ReadVector();
            var multiplication = FourierTransform.MultiplyPolinomials(polinomial1, polinomial2);
            Console.WriteLine("Итоговый вектор:");
            foreach (var coeff in multiplication)
                Console.Write(coeff + " ");
            Console.ReadKey();
        }
    }
}
