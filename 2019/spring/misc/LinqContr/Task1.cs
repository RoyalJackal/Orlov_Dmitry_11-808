using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContrLinq
{
    public static class Task1
    {
        public static string[] Solve(string[] A)
        {
            int index = 0;
            return A
                .Select(s =>
                {
                    //увеличивем индекс и прибавляем его к строке
                    index++;
                    return s + index;
                })
                //отсеиваем те, где есть только число
                .Where(s => s.Length > 1)
                .OrderBy(s => s)
                .ToArray();
        }
    }
}
