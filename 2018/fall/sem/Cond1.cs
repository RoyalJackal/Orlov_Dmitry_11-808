using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cond1
{
    class Program
    {
        static void Main(string[] args)
        {
            string startPosition = Console.ReadLine();
            string endPosition = Console.ReadLine();
            bool bishop = (Math.Abs(startPosition[0] - endPosition[0]) == Math.Abs(startPosition[1] - endPosition[1]) &&
                Math.Abs(startPosition[0] - endPosition[0]) != 0);
            bool knight = (Math.Abs(startPosition[0] - endPosition[0]) == 2 && Math.Abs(startPosition[1] - endPosition[1]) == 1 ||
                Math.Abs(startPosition[0] - endPosition[0]) == 1 && Math.Abs(startPosition[1] - endPosition[1]) == 2); 
            bool rook = (startPosition[0] != endPosition[0] && startPosition[1] == endPosition[1] ||
                startPosition[0] == endPosition[0] && startPosition[1] != endPosition[1]);
            bool queen = (Math.Abs(startPosition[0] - endPosition[0]) == Math.Abs(startPosition[1] - endPosition[1]) ||
                startPosition[0] != endPosition[0] && startPosition[1] == endPosition[1] ||
                startPosition[0] == endPosition[0] && startPosition[1] != endPosition[1] &&
                (Math.Abs(startPosition[0] - endPosition[0]) != 0 || Math.Abs(startPosition[1] - endPosition[1]) != 0));
            bool king = (Math.Abs(startPosition[0] - endPosition[0]) <= 1 && Math.Abs(startPosition[1] - endPosition[1]) <= 1 && 
                (Math.Abs(startPosition[0] - endPosition[0]) == 1 || Math.Abs(startPosition[1] - endPosition[1]) == 1));
            Console.WriteLine("Слон {0}, Конь {1}, Ладья {2}, Ферзь {3}, Король {4}", bishop, knight, rook, queen, king);
            Console.ReadKey();
        }
    }
}
