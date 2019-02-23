using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farey
{
    //класс дроби
    class Fraction : IComparable
    {
        public int Enumerator;
        public int Denominator;

        //конструктор, принимающий числитель и знаменатель
        public Fraction(int enumerator, int denomenator)
        {
            Enumerator = enumerator;
            Denominator = denomenator;
        }

        //сравнивает значение дроби с другим объектом
        public int CompareTo(object obj)
        {
            return Evaluate().CompareTo(obj);
        }

        //выводит значение дроби, равное нецелому частному её числителя и знаменателя
        public double Evaluate()
        {
            return (double)(Enumerator / Denominator);
        }

        //выписывает дробь в виде enum/deno
        public void Write()
        {
            Console.Write("{0}/{1}", Enumerator, Denominator);
        }
    }
}
