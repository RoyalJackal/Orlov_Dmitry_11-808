using System;
using System.IO;
using System.Collections.Generic;

namespace Heapsort
{
    public static class Generator
    {
        //метод генерации файлов, заполенных случайными значениями
        public static void Generete()
        {
            var rnd = new Random();
            var lines = new List<string>();
            for (int i = 1; i <= 100; i++)
            {
                //заполняем лист случайными 100*(номер файла) элементами и записываем в файл
                for (int j = 1; j <= i * 100; j++)
                    lines.Add(rnd.Next().ToString());
                File.WriteAllLines(String.Format("data{0}.txt", i), lines);
                //после очередного файла чистим лист
                lines.Clear();
            }
        }
    }
}