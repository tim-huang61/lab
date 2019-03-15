using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> list,
            Func<TSource, bool> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> list,
            Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (var item in list)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> list, Func<TSource, TResult> predicate)
        {
            foreach (var item in list)
            {
                yield return predicate(item);
            }
        }
    }
}