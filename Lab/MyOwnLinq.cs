using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> list,
            Func<TSource, bool> predicate)
        {
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    yield return current;
                }
            }=
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

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> list,
            Func<TSource, TResult> predicate)
        {
            foreach (var item in list)
            {
                yield return predicate(item);
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> list,
            Func<TSource, int, TResult> predicate)
        {
            int index = 0;
            foreach (var item in list)
            {
                yield return predicate(item, index);
                index++;
            }
        }
    }
}