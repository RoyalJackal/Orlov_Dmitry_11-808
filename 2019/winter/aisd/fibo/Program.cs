using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibo
{
    //класс матрицы 2х2
    public class Matrix
    {
        public int[,] Matr;
        
        //заполняет матрицу четыремя входящими значениями
        public Matrix(int upperFirst, int upperSecond, int lowerFirst, int lowerSecond)
        {
            Matr = new int[2,2];
            Matr[0,0] = upperFirst;
            Matr[0,1] = upperSecond;
            Matr[1,0] = lowerFirst;
            Matr[1,1] = lowerSecond;
        }

        //возвращает произведениие заданной матрицы на другую
        public Matrix Multiply(Matrix secMatr)
        {
            int upperFirst = Matr[0,0] * secMatr.Matr[0,0] + Matr[0,1] * secMatr.Matr[1,0];
            int upperSecond = Matr[0,0] * secMatr.Matr[1,0] + Matr[0,1] * secMatr.Matr[1,1];
            int lowerFirst = Matr[1,0] * secMatr.Matr[0,0] + Matr[1,1] * secMatr.Matr[0,1];
            int lowerSecond = Matr[1,0] * secMatr.Matr[1,0] + Matr[1,1] * secMatr.Matr[1,1];
            return new Matrix(upperFirst, upperSecond, lowerFirst, lowerSecond);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //считываем номер числа фибоначчи и превращаем его в бинарный код
            int fuboNumber = Int32.Parse(Console.ReadLine());
            var binaryCode = Convert.ToString(fuboNumber, 2);
            //задаём матрицу для счёта числа фибоначчи и еденичную матрицу
            var baseMatrix = new Matrix(0, 1, 1, 1);
            var matrix = new Matrix(1, 0, 0, 1);
            for (int i = 0; i < binaryCode.Length; i++)
            {
                //если значение очередного разряда бинарного кода равно 1
                //то умножаем матрицу на себя и на базовую матрицу,
                //иначе просто умножаем её на себя
                matrix = matrix.Multiply(matrix);
                if (binaryCode[i] == '1')
                    matrix = matrix.Multiply(baseMatrix);
            }
            Console.WriteLine(matrix.Matr[0,1]);
            Console.ReadKey();
        }
    }
}
