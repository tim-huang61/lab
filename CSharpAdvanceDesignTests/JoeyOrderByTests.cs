using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, string> keySelector, IComparer<string> keyCompare)
        {
            KeySelector = keySelector;
            KeyCompare = keyCompare;
        }

        public Func<Employee, string> KeySelector { get; private set; }
        public IComparer<string> KeyCompare { get; private set; }

        public int Compare(Employee employee, Employee minElement)
        {
            return KeyCompare.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        [Ignore("")]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            IComparer<string> firstKeyCompare = Comparer<string>.Default;
            Func<Employee, string> secondKeySelector = employee1 => employee1.FirstName;
            var actual = JoeyOrderByLastName(employees,
                new CombineKeyComparer(employee => employee.LastName, firstKeyCompare),
                new CombineKeyComparer(secondKeySelector, firstKeyCompare));

            var expected = new[]
            {
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_last_name_and_first_name()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            IComparer<string> firstKeyCompare = Comparer<string>.Default;
            Func<Employee, string> secondKeySelector = employee1 => employee1.FirstName;
            var actual = JoeyOrderByLastName(employees,
                new CombineKeyComparer(employee => employee.LastName, firstKeyCompare),
                new CombineKeyComparer(secondKeySelector, firstKeyCompare));
            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };


            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastName(IEnumerable<Employee> employees,
            CombineKeyComparer firstComparer, CombineKeyComparer secondComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];
                    var firstCompareResult = firstComparer.Compare(employee, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        if (secondComparer.KeyCompare.Compare(secondComparer.KeySelector(employee),
                                secondComparer.KeySelector(minElement)) < 0)
                        {
                            minElement = employee;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}