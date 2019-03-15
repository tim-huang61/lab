using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> list, Func<TSource, bool> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}