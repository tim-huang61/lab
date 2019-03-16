using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        //[Ignore("")]
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
//            var actual = JoeyOrderBy(employees,
//                new ComboCompare(new CombineKeyComparer<string>(employee => employee.LastName, firstKeyCompare),
//                    new CombineKeyComparer<string>(secondKeySelector, firstKeyCompare)));

            var actual = JoeyOrderByKeepCompare(employees, e => e.LastName, Comparer<string>.Default);
                //.JoeyThenBy(e => e.FirstName, Comparer<string>.Default);

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
        public void orderBy_last_name_and_first_name_and_age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            // 裝飾ICompare
//            var firstComparer = new CombineKeyComparer<string>(employee => employee.LastName, Comparer<string>.Default);
//            var secondComparer = new CombineKeyComparer<string>(employee1 => employee1.FirstName, Comparer<string>.Default);
//            var firstCombo = new ComboCompare(firstComparer, secondComparer);
//            var thirdComparer = new CombineKeyComparer<int>(e => e.Age, Comparer<int>.Default);
//            var finalCombo = new ComboCompare(firstCombo, thirdComparer);
//            var actual = JoeyOrderBy(employees, finalCombo);


             
            var actual = JoeyOrderByKeepCompare(employees, e => e.LastName, Comparer<string>.Default)
                .JoeyThenBy(e => e.FirstName, Comparer<string>.Default)
                .JoeyThenBy(e => e.Age, Comparer<int>.Default);

            
            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };


            expected.ToExpectedObject().ShouldMatch(actual);
        }       



        private IOrderedEnumerable<Employee> JoeyOrderByKeepCompare<TKey>(IEnumerable<Employee> employees, Func<Employee, TKey> predicate, Comparer<TKey> compare)
        {
            return new MyOrderedEnumerable<TKey>(employees, new CombineKeyComparer<TKey>(predicate, compare));
        }

        private IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees, ComboCompare comboCompare)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];
                    if (comboCompare.Compare(employee, minElement) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
        
        private IEnumerable<Employee> JoeyOrderByKeepCompare(IEnumerable<Employee> employees, ComboCompare comboCompare)
        {
//            var elements = employees.ToList();
//            while (elements.Any())
//            {
//                var minElement = elements[0];
//                var index = 0;
//                for (int i = 1; i < elements.Count; i++)
//                {
//                    var employee = elements[i];
//                    if (comboCompare.Compare(employee, minElement) < 0)
//                    {
//                        minElement = employee;
//                        index = i;
//                    }
//                }
//
//                elements.RemoveAt(index);
//                yield return minElement;
//            }

//new MyOrderedEnumerable()

            return null;
        }
    }
}