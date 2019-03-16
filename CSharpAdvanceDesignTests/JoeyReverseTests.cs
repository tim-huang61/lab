using System.Collections;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "David", LastName = "Wang"},
            };

            var actual = employees.JoeyReverse();

            var expected = new List<Employee>
            {
                new Employee() {FirstName = "David", LastName = "Wang"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "Joey", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void reserve_nums()
        {
            var nums = new[] {1, 2, 3};
            var actual = nums.JoeyReverse();

            new[] {3, 2, 1}.ToExpectedObject().ShouldMatch(actual);
        }
    }
}