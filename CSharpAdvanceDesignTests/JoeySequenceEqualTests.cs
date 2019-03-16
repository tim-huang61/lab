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
            var first = new List<int> {3, 2};
            var second = new List<int> {3, 2, 0};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();
                if (IsLengthDiff(firstFlag, secondFlag))
                {
                    return false;
                }

                if (IsValueDiff(firstEnumerator, secondEnumerator))
                {
                    return false;
                }

                if (IsEnd(firstFlag))
                {
                    return true;
                }
            }
        }

        private bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private bool IsValueDiff(IEnumerator<int> firstEnumerator, IEnumerator<int> secondEnumerator)
        {
            return firstEnumerator.Current != secondEnumerator.Current;
        }

        private bool IsLengthDiff(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }
    }
}