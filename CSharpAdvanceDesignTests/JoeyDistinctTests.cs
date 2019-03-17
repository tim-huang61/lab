using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = Distinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            var dictionary = new Dictionary<int,int>();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!dictionary.TryGetValue(current, out var value))
                {
                    dictionary.Add(current, current);
                    yield return current;
                }
            }
        }
    }
}