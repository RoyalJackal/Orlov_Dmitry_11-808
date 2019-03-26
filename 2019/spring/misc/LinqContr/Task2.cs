using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContrLinq
{
    public static class Task2
    {
        public static IEnumerable<Tuple<T, T>> Solve<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, Func<T, T, bool> func)
        {
            //для каждой пары применяем функцию
            foreach (var item1 in seq1)
                foreach (var item2 in seq2)
                    if (func(item1, item2))
                        yield return Tuple.Create(item1, item2);
        }
    }
}
