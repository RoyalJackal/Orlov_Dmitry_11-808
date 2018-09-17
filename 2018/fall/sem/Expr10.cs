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
            Console.WriteLine("Введите числа для нахождения суммы всех положительных чисел меньше 1000 кратных этим числам");
            int number1 = int.Parse(Console.ReadLine());
            int number2 = int.Parse(Console.ReadLine());
            int sum = AriphmeticProgressionSum(number1) + AriphmeticProgressionSum(number2) - AriphmeticProgressionSum(number1*number2);
            Console.WriteLine("Сумма всех положительных чисел меньше 1000 кратных {0} или {1} равна {2}",number1, number2, sum);
            Console.ReadKey();
        }
        //метод, считывающий сумму элементов арифметической прогрессии меньше 1000
        static int AriphmeticProgressionSum(int number)
        {
            return (2 * number + (999 / number - 1) * number) * (999 / number) / 2;
        }

    }
}
