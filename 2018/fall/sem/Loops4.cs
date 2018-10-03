using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops4
{
    class Program
    {
        static void Main(string[] args)
        {
            int arrayLenght = Int32.Parse(Console.ReadLine());
            int[] array = new int[arrayLenght];
            for (int i = 0; i < arrayLenght; i++)
                array[i] = Int32.Parse(Console.ReadLine());
            int max = arrayLenght / 2;
            int currentLenght = 1;
            for (int i = 1; i < arrayLenght; i++)
            {
                if (array[i] == array[i - 1]) currentLenght++;
                else
                {
                    if (max < currentLenght) max = currentLenght;
                    currentLenght = 1;
                }
                if (max < currentLenght && i + 1 == arrayLenght) max = currentLenght;
            }
            Console.WriteLine();
            Console.WriteLine(max);
            Console.ReadKey();
        }
    }
}
