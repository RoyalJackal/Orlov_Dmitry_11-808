using System;
using System.Numerics;

namespace PolynomialMultiplication
{
    public static class FourierTransform
    {
        //проводит трансформацию фурье над вектором комплексных чисел
        public static Complex[] Transform(Complex[] input)
        {
            //при длине 2 прекращаем рекурсию и считаем по формуле
            if (input.Length == 2) return new Complex[] { input[0] + input[1], input[0] - input[1] };

            //вычисление корней в виде комплексных корней из 1
            var roots = new Complex[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                double alpha = 2.00 * Math.PI * i / input.Length;
                roots[i] = new Complex(Math.Cos(alpha), Math.Sin(alpha));
            }

            //делим вектор на две части и проводим над ними трансформацию
            var firstHalf = new Complex[input.Length / 2];
            var secondHalf = new Complex[input.Length / 2];
            for (int i = 0; i < input.Length / 2; i++)
            {
                firstHalf[i] = input[i * 2];
                secondHalf[i] = input[i * 2 + 1];
            }
            firstHalf = Transform(firstHalf);
            secondHalf = Transform(secondHalf);

            //считаем новые коэффициенты по формуле
            var result = new Complex[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = firstHalf[i % (input.Length / 2)] + 
                    roots[i] * secondHalf[i % (input.Length / 2)];
            }
            return result;
        }

        //проводит обратную трансформацию фурье над вектором комплексных чисел
        public static Complex[] ReverseTransform(Complex[] input)
        {
            //проводим трансформацию фурье, делим каждый элемент на количество элементов
            var result = Transform(input);
            for (int i = 0; i < result.Length; i++)
                result[i] /= input.Length;
            //переворачивем вектор с первого до последнего элемента (при счёте с нуля)
            for (int i = 1; i < result.Length / 2; i++)
            {
                var swap = result[i];
                result[i] = result[result.Length - i];
                result[result.Length - i] = swap;
            }
            //вычитаем эпсилон чтобы увеличить точность вычислений
            for (int i = 0; i < result.Length; i++)
            {
                var epsilonReal = result[i].Real % 1E-6;
                var epsilonImaginary = result[i].Imaginary % 1E-6;
                result[i] -= new Complex(epsilonReal, epsilonImaginary);
            }
            return result;
        }

        //перемножение двух полиномов
        public static Complex[] MultiplyPolinomials(Complex[] p1, Complex[] p2)
        {
            Complex[] vector1;
            Complex[] vector2;
            //определение наибольшего из полиномов и присванивание его длины другому
            if (p1.Length > p2.Length)
            {
                vector1 = p1;
                vector2 = EnlargeVector(p2, p1.Length);
            }
            else if (p1.Length < p2.Length)
            {
                vector1 = EnlargeVector(p1, p2.Length);
                vector2 = p2;      
            }
            else
            {
                vector1 = p1;
                vector2 = p2;
            }
            //находим близжайшую степень двойки и расширяем вектора до этой степени + 1
            //т.к. при умножении двух полиномов n степени получаем полином степени 2n
            var powOfTwo = GetClosestPowerOfTwo(vector1.Length);
            vector1 = EnlargeVector(vector1, powOfTwo * 2);
            vector2 = EnlargeVector(vector2, powOfTwo * 2);
            vector1 = Transform(vector1);
            vector2 = Transform(vector2);
            //почленно умножаем элементы и получаем обратную транформацию полученного вектора
            var result = new Complex[vector1.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = vector1[i] * vector2[i];
            return ReverseTransform(result);

        }

        //метод расширения вектора до нужного размера
        public static Complex[] EnlargeVector(Complex[] vector, int length)
        {
            var result = new Complex[length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i];
            }
            for (int i = vector.Length; i < result.Length; i++)
                result[i] = new Complex(0, 0);
            return result;
        }

        //метод нахождения близжайшей степени двойки к числу
        public static int GetClosestPowerOfTwo(int number)
        {
            var result = 2;
            while (result < number)
                result *= 2;
            return result;
        }
    }
}
