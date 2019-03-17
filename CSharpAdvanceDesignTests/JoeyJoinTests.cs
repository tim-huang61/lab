using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee {FirstName = "David", LastName = "Chen"};
            var joey = new Employee {FirstName = "Joey", LastName = "Chen"};
            var tom = new Employee {FirstName = "Tom", LastName = "Chen"};

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, current1 => current1, pet2 => pet2.Owner, EqualityComparer<Employee>.Default, (current, pet1) => new Tuple<string, string>(current.FirstName, pet1.Name));

            employees.Join(pets, employee => employee.FirstName, pet => pet.Owner.FirstName,
                (employee, pet) => Tuple.Create(employee.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void all_pets_and_owner_with_fullName()
        {
            var david = new Employee {FirstName = "David", LastName = "Li"};
            var joey = new Employee {FirstName = "Joey", LastName = "Chen"};
            var tom = new Employee {FirstName = "Tom", LastName = "Wang"};

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, current => current, pet1 => pet1.Owner, EqualityComparer<Employee>.Default, (employee, pet) => $"{pet.Name}-{employee.LastName}");

            var expected = new[]
            {
                "Didi-Li",
                "Lala-Chen",
                "QQ-Chen",
                "Fufu-Wang",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void all_pets_and_owner_with_fullName2()
        {
            var david = new Employee {FirstName = "David", LastName = "Li"};
            var joey = new Employee {FirstName = "Joey", LastName = "Chen"};
            var tom = new Employee {FirstName = "Tom", LastName = "Wang"};

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Joey-Lala"},
                new Pet() {Name = "David-Didi"},
                new Pet() {Name = "Tom-Fufu"},
                new Pet() {Name = "Joey-QQ"},
            };

            var actual = JoeyJoin(employees, pets, 
                employee => employee.FirstName, 
                pet => pet.Name.Split('-')[0], EqualityComparer<string>.Default, (employee, pet) => $"{employee.FirstName[0]}-{pet.Name.Split('-')[1]}");

            var expected = new[]
            {
              "D-Didi",
              "J-Lala",
              "J-QQ",
              "T-Fufu"
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        
        private IEnumerable<TResult> JoeyJoin<TOuter,TInner,TKey, TResult>(IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerSelector,
            Func<TInner, TKey> innerSelector,
            IEqualityComparer<TKey> equalityComparer,
            Func<TOuter, TInner, TResult> selector)
        {
            var outerEnumerator = outer.GetEnumerator();
            var innerEnumerator = inner.GetEnumerator();
            while (outerEnumerator.MoveNext())
            {
                var current = outerEnumerator.Current;
                while (innerEnumerator.MoveNext())
                {
                    var pet = innerEnumerator.Current;
                    if (equalityComparer.Equals(outerSelector(current), innerSelector(pet)))
                    {
                        yield return selector(current, pet);
                    }
                }

                innerEnumerator.Reset();
            }
        }
    }
}