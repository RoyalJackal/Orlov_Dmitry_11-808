using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farey
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите значение максимального знаменателя");
            int n = Int32.Parse(Console.ReadLine());
            var farey = CreateFareySequence(n);
            foreach (var item in farey)
            {
                item.Write();
                Console.Write(' ');
            }
            Console.ReadKey();
        }

        //создаёт ряд фарея с заданным максимальным знаменателем
        public static LinkedList<Fraction> CreateFareySequence(int maxDemominator)
        {
            //создаём новый список и добавляем в него 0 и 1
            LinkedList<Fraction> farey = new LinkedList<Fraction>();
            farey.AddFirst(new Fraction(0, 1));
            farey.AddFirst(new Fraction(1, 1));
            //проходим через каждое промежуточное значение знаменателя
            for (int i = 1; i < maxDemominator; i++)
            {
                LinkedListNode<Fraction> node = farey.First;
                while (node.Next != null)
                {
                    //проходим по каждому элементу и проверяем,
                    //равна ли сумма их знаметалелей нынешнему максимальному
                    //если равна, то ставим между ними новый элемент
                    //значения числителя и знаменателя которого равно
                    //сумме значений числителей и знаменателей этих соседних элементов
                    if (node.Value.Denominator + node.Next.Value.Denominator == i)
                    {
                        farey.AddAfter(node, new Fraction(node.Value.Enumerator + node.Next.Value.Enumerator, 
                            node.Value.Denominator + node.Next.Value.Denominator));
                        node = node.Next;
                    }
                    node = node.Next;
                }
            }
            return farey;
        }
    }
}
