﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab;
using Lab.Entities;

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

            var actual = first.JoeySequenceEqual(second);

            Assert.IsTrue(actual);
        }


        [Test]
        public void compare_two_numbers_equal2()
        {
            var first = new List<int> {3, 2, 1};
            var second = new List<int> {1, 2, 3};

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal3()
        {
            var first = new List<int> {3, 2};
            var second = new List<int> {3, 2, 1};

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal4()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal5()
        {
            var first = new List<int> {3, 2};
            var second = new List<int> {3, 2, 0};

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void two_employees()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "David", LastName = "Wang"},
            };

            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "David", LastName = "Wang"},
            };

            var actual = first.JoeySequenceEqual(second, new JoeyEmployeeWithPhoneEqualityComparer());

            Assert.IsTrue(actual);
        }

        [Test]
        public void dictionary_test()
        {
//            var a = new Employee() {FirstName = "Joey", LastName = "Chen"};
//            var b = new Employee() {FirstName = "Joey", LastName = "Chen"};
//            var c = new Employee() {FirstName = "David", LastName = "Wang"};
//
//            var dictionary = new Dictionary<Employee, int>(new JoeyEmployeeWithPhoneEqualityComparer());
//            dictionary.Add(a, 1);
//            dictionary.Add(b, 2);
//            dictionary.Add(c, 3);

            Assert.Pass();
        }


        //1. 可以將判斷式抽成Func 2. 實作IEqualityComparer
        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
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

                if (IsEnd(firstFlag))
                {
                    return true;
                }

                var firstEnumeratorCurrent = firstEnumerator.Current;
                var secondEnumeratorCurrent = secondEnumerator.Current;
                if (!firstEnumeratorCurrent.Equals(secondEnumeratorCurrent))
                {
                    return false;
                }
            }
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsValueDiff<TSource>(IEnumerator<TSource> firstEnumerator,
            IEnumerator<TSource> secondEnumerator)
        {
            return !firstEnumerator.Current.Equals(secondEnumerator.Current);
        }

        private static bool IsLengthDiff(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }
    }
}