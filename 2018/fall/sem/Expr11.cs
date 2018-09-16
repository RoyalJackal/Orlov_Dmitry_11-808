using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите время");
            String time = Console.ReadLine();
            //деление строчки со временем на часы и минуты и присовение соотвествующим переменным
            String [] parts = time.Split(':',' ','.',',');
            int hour = int.Parse(parts[0]);
            //приведение к 12-часовому формату
            if (hour>12) { hour -= 12; };
            int minute = int.Parse(parts[1]);
            //вывод с выбором наименьшего угла
            if (Math.Abs(30 * hour - 6 * minute) > 180)
            { Console.WriteLine("Угол от часовой к минутной стрелке равен " + (360 - Math.Abs(30 * hour - 6 * minute))); }
            else
            { Console.WriteLine("Угол от часовой к минутной стрелке равен " + (Math.Abs(30 * hour - 6 * minute))); };
            Console.ReadKey();
        }
    }
}
