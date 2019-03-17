using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyContainsTests
    {
        [Test]
        public void contains_joey_chen()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Wang"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "Joey", LastName = "Chen"},
            };

            var joey = new Employee() {FirstName = "Joey", LastName = "Chen"};

            var actual = JoeyContains(employees, joey);

            Assert.IsTrue(actual);
        }

        private bool JoeyContains(IEnumerable<Employee> employees, Employee value)
        {
            var enumerator = employees.GetEnumerator();
            var comparer = new JoeyEmployeeEqualityComparer();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (comparer.Equals(current, value))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class JoeyEmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            return new {obj.LastName, obj.FirstName}.GetHashCode();
        }
    }
}