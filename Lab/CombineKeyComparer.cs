using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> keySelector, IComparer<TKey> keyCompare)
        {
            KeySelector = keySelector;
            KeyCompare = keyCompare;
        }

        private Func<Employee, TKey> KeySelector { get; }
        private IComparer<TKey> KeyCompare { get; }

        public int Compare(Employee employee, Employee minElement)
        {
            return KeyCompare.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }
}