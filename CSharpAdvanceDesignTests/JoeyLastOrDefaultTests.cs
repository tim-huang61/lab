using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = employees.JoeyLastOrDefault();
            Assert.IsNull(actual);
        }
        [Test]
        public void get_last_Employee()
        {
            var employees = new List<Employee>
            {
                new Employee{ FirstName = "Joey", LastName = "Chen"},
                new Employee{ FirstName = "Cash", LastName = "Wu"},
                new Employee{ FirstName = "David", LastName = "Wu"},
            };
            var actual = employees.JoeyLastOrDefault();
            var expected = new Employee {FirstName = "David", LastName = "Wu"};
            actual.ToExpectedObject().ShouldEqual(expected);
        }
    }
}