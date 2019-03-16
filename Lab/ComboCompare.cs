using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboCompare<TSource> : IComparer<TSource>
    {
        public ComboCompare(IComparer<TSource> firstComparer, IComparer<TSource> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<TSource> FirstComparer { get; private set; }
        public IComparer<TSource> SecondComparer { get; private set; }

        public int Compare(TSource x, TSource y)
        {
            var firstCompare = FirstComparer.Compare(x, y);

            return firstCompare == 0 ? SecondComparer.Compare(x, y) : firstCompare;
        }
    }
}