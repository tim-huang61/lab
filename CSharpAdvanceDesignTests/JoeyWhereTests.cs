using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void group_sum_count_is_3_sum_cost()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var expected = new[] {63, 153, 89};
            var actual = JoeyGroupSum(products);
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyGroupSum(IEnumerable<Product> products)
        {
            // origin
            // return products.GroupBy(p => Math.Ceiling(p.Id / 3.0)).Select(item => item.Sum(i => i.Cost)).ToArray();

            // improved
            int pageIndex = 0;
            int pageSize = 3;
            while (products.Count() >= pageSize * pageIndex)
            {
                yield return products.Skip(pageSize * pageIndex).Take(pageSize).Sum(p => p.Cost);
                pageIndex++;
            }
        }

        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.JoeyWhere(product => product.Price > 200 && product.Price < 500);
            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_over_30()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.JoeyWhere(p => p.Price > 200 && p.Price < 500 && p.Cost > 30);

            var expected = new List<Product>
            {
                //new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_name_length_less_than_5()
        {
            var names = new[] {"Joye", "Cash", "Tim", "William", "Brain", "Jessica"};
            var actual = names.JoeyWhere((name, index) => name.Length < 5);
            var expected = new[] {"Joye", "Cash", "Tim"};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_name_is_even_index()
        {
            var names = new[] {"Joye", "Cash", "Tim", "William", "Brain", "Jessica"};
            var actual = names.JoeyWhere((name, index) => index % 2 == 0);
            var expected = new[] {"Joye", "Tim", "Brain"};

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}