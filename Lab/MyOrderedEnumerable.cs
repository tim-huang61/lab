using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderedEnumerable<TKey> : IOrderedEnumerable<Employee>
    {
        private readonly IEnumerable<Employee> _employees;
        private readonly CombineKeyComparer<TKey> _combineKeyComparer;

        public MyOrderedEnumerable(IEnumerable<Employee> employees, CombineKeyComparer<TKey> combineKeyComparer)
        {
            _employees = employees;
            _combineKeyComparer = combineKeyComparer;
        }
        
        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector, IComparer<TKey> comparer, bool @descending)
        {
            var combineKeyComparer = new CombineKeyComparer<TKey>(keySelector, comparer);
            
            return new MyOrderedEnumerable<TKey>(_employees, combineKeyComparer);
        }

        public IEnumerator<Employee> GetEnumerator()
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