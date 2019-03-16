using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
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

            Comparer<string> firstKeyCompare = Comparer<string>.Default;
            var actual = JoeyOrderByLastName(employees, employee => employee.LastName, firstKeyCompare, employee1 => employee1.FirstName, firstKeyCompare);

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

            Comparer<string> firstKeyCompare = Comparer<string>.Default;
            var actual = JoeyOrderByLastName(employees, employee => employee.LastName, firstKeyCompare, employee1 => employee1.FirstName, firstKeyCompare);
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
            Func<Employee, string> firstKeySelector, 
            Comparer<string> firstKeyCompare, 
            Func<Employee, string> secondKeySelector, 
            Comparer<string> secondKeyCompare)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];
                    if (firstKeyCompare.Compare(firstKeySelector(employee), firstKeySelector(minElement)) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                    else if (firstKeyCompare.Compare(firstKeySelector(employee), firstKeySelector(minElement)) == 0)
                    {
                        if (secondKeyCompare.Compare(secondKeySelector(employee), secondKeySelector(minElement)) < 0)
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