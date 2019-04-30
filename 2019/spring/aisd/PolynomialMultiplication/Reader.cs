using System;
using System.Collections.Generic;
using System.Numerics;

namespace PolynomialMultiplication
{
    public static class Reader
    {
        public static Complex[] ReadVector()
        {
            var reader = new List<Complex>();
            Console.WriteLine("Начинайте вводить вектор комплексных чисел через пробел.");
            Console.WriteLine("Первое число должно быть реальной частью, второе комплексной");
            Console.WriteLine("Чтобы закончить, введите пустую строчку");
            string input = Console.ReadLine();
            while (input != "")
            {
                var parts = input.Split(' ');
                if (parts.Length != 2)
                    throw new Exception("Введено неверное количество чисел");
                var flag = double.TryParse(parts[0], out double real);
                if (!flag) throw new Exception("Введённые числа имеют неверный вид");
                flag = double.TryParse(parts[1], out double imaginary);
                if (!flag) throw new Exception("Введённые числа имеют неверный вид");
                reader.Add(new Complex(real, imaginary));
                input = Console.ReadLine();
            }
            return reader.ToArray();
        }
    }
}
