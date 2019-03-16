using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal1()
        {
            var first = new List<int> {3, 2, 1};
            var second = new List<int> {3, 2, 1};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }


        [Test]
        public void compare_two_numbers_equal2()
        {
            var first = new List<int> {3, 2, 1};
            var second = new List<int> {1, 2, 3};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal3()
        {
            var first = new List<int> {3, 2};
            var second = new List<int> {3, 2, 1};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal4()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }
        
        [Test]
        public void compare_two_numbers_equal5()
        {
            var first = new List<int> { 3,2};
            var second = new List<int> { 3,2,0};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                var firstEnumeratorCurrent = firstEnumerator.Current;
                var secondEnumeratorCurrent = secondEnumerator.Current;
                if (!(firstEnumerator.MoveNext() && firstEnumerator.MoveNext()))
                {
                    return false;
                }
                
                return firstEnumeratorCurrent == secondEnumeratorCurrent;
            }

            return true;
        }
    }
}