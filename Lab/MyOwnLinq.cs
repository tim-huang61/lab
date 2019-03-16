using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lab.Entities;

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

        //也可以用yield break;
        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int takeCount)
        {
            int index = 0;
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext() && index < takeCount)
            {
                yield return enumerator.Current;
                index++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> source, int skipCount)
        {
            var enumerator = source.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (index >= skipCount)
                {
                    yield return current;
                }

                index++;
            }
        }

        public static IEnumerable<TSource> JoeyTakeWhile<TSource>(this IEnumerable<TSource> cards,
            Func<TSource, bool> predicate)
        {
            var enumerator = cards.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var card = enumerator.Current;
                if (!predicate(card))
                {
                    yield break;
                }

                yield return card;
            }
        }

        public static bool JoeyAll<TSource>(this IEnumerable<TSource> girls, Func<TSource, bool> predicate)
        {
            var enumerator = girls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var girl = enumerator.Current;
                if (!predicate(girl))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> products, Func<TSource, bool> predicate)
        {
            var enumerator = products.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> products)
        {
            var enumerator = products.GetEnumerator();

            return enumerator.MoveNext();
        }

        public static T JoeyFirstOrDefault<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();

            return enumerator.MoveNext() ? enumerator.Current : default(T);
        }

        public static T JoeyFirstOrDefault<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }

            return default(T);
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();
            var item = default(TSource);
            while (enumerator.MoveNext())
            {
                item = enumerator.Current;
            }

            return item;
        }

        public static IEnumerable<TSource> JoeyReverse<TSource>(this IEnumerable<TSource> source)
        {
            return new Stack<TSource>(source);
        }

        public static IEnumerable<TResult> JoeyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first
            , IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> predicate)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return predicate(firstEnumerator.Current, secondEnumerator.Current);
            }
        }
    }
}