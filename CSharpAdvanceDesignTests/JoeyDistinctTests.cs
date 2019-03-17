using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Lab;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = JoyeDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
        [Test]
        public void distinct_employees()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Wang"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Joey", LastName = "Chen"},
            };
            var actual = JoyeDistinctWithEqualityComparer(employees, new JoeyEmployeeEqualityComparer());

            var expected = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Wang"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "Joey", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        // hashset 可消除重複
        private IEnumerable<T> JoyeDistinct<T>(IEnumerable<T> numbers)
        {
//            var hashSet = new HashSet<T>();
//            var enumerator = numbers.GetEnumerator();
//            while (enumerator.MoveNext())
//            {
//                var current = enumerator.Current;
//                if (hashSet.Add(current))
//                {
//                    yield return current;
//                }
//            }
            return JoyeDistinctWithEqualityComparer(numbers, EqualityComparer<T>.Default);
        }
        
        private IEnumerable<TSource> JoyeDistinctWithEqualityComparer<TSource>(IEnumerable<TSource> employees, IEqualityComparer<TSource> comparer)
        {
            var hashSet = new HashSet<TSource>(comparer);
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}