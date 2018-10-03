using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            bool numberIsStarted = false;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] != '0')
                {
                    Console.Write(input[i]);
                    numberIsStarted = true;
                }
                else
                    if (input[i] == '0' && !numberIsStarted) ;
                    else Console.Write(input[i]);
            }
            Console.ReadKey();
        }
    }
}
