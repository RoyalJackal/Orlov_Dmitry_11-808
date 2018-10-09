using System;
using System.Linq;

namespace Names
{
    internal static class InterestingAboutNames
    {
        public static HistogramData GetBirthRatePerMonth(NameData[] names, double year)
        {
            //создание и заполнение массива с номерами месяцов
            var month = CreateStringArray(12);
            //создание массива для счёта количества рождений в месяце
            var birthCount = new double[12];
            foreach (var name in names)
                //проверка года и последующее увеличение счётчика рождений
                if (name.BirthDate.Year == year)
                    birthCount[name.BirthDate.Month - 1]++;
            return new HistogramData(string.Format("Рождаемость по месяцам в {0} году", year), month, birthCount);
        }

        //метод по созданию строкового массива из length элементов и заполнению его значениями от 1 до length
        public static string[] CreateStringArray(int length)
        {
            var array = new string[length];
            for (int i = 0; i < length; i++)
                array[i] = (i + 1).ToString();
            return array;
        }
    }
}
