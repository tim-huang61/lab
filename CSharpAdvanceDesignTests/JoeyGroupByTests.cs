using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Lee"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Lee"},
            };

            var actual = JoeyGroupBy(employees, employee => employee.LastName);
            Assert.AreEqual(2, actual.Count());
            var firstGroup = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees, Func<Employee, string> keySelector)
        {
            var myLookup = new MyLookup();
            foreach (var employee in employees)
            {
                myLookup.AddElement(keySelector(employee), employee);
            }
            
            return myLookup;
        }
    }

    internal class MyLookup : IEnumerable<IGrouping<string, Employee>>
    {
        private readonly IEnumerable<Employee> employees;
        private readonly Func<Employee, string> keySelector;
        private Dictionary<string, List<Employee>> _lookup = new Dictionary<string, List<Employee>>();

        public IEnumerator<IGrouping<string, Employee>> GetEnumerator()
        {
            return _lookup.Select(x => new MyGrouping(x.Key, x.Value)).GetEnumerator();
        }

        public void AddElement(string key, Employee employee)
        {
            if (_lookup.ContainsKey(key))
            {
                _lookup[key].Add(employee);
            }
            else
            {
                _lookup.Add(key, new List<Employee> {employee});
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        private IEnumerable<IGrouping<string, Employee>> ConvertMultiGrouping(Dictionary<string, List<Employee>> lookup)
        {
            var enumerator = lookup.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var keyValuePair = enumerator.Current;
                
                yield return new MyGrouping(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }

    public class MyGrouping : IGrouping<string, Employee>
    {
        private readonly IEnumerable<Employee> _collection;

        public MyGrouping(string key, IEnumerable<Employee> collection)
        {
            Key = key;
            _collection = collection;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }
    }
}