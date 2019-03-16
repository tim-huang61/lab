using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> _employees;
        private readonly IComparer<TSource> _combineKeyComparer;

        public MyOrderedEnumerable(IEnumerable<TSource> employees, IComparer<TSource> combineKeyComparer)
        {
            _employees = employees;
            _combineKeyComparer = combineKeyComparer;
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer, bool @descending)
        {
            var combineKeyComparer = new CombineKeyComparer<TKey, TSource>(keySelector, comparer);

            return new MyOrderedEnumerable<TSource>(_employees, new ComboCompare<TSource>(_combineKeyComparer, combineKeyComparer));
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            var elements = _employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];
                    if (_combineKeyComparer.Compare(employee, minElement) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}