using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineKeyComparer<TKey, TSource> : IComparer<TSource>
    {
        public CombineKeyComparer(Func<TSource, TKey> keySelector, IComparer<TKey> keyCompare)
        {
            KeySelector = keySelector;
            KeyCompare = keyCompare;
        }

        private Func<TSource, TKey> KeySelector { get; }
        private IComparer<TKey> KeyCompare { get; }

        public int Compare(TSource employee, TSource minElement)
        {
            return KeyCompare.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }
}