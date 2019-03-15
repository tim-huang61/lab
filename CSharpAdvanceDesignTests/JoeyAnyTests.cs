using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        [Test]
        public void three_products()
        {
            var products = new[]
            {
                new Product(),
                new Product(),
                new Product(),
            };

            Assert.IsTrue(products.JoeyAny());
        }

        [Test]
        public void empty_employees()
        {
            var emptyProducts = new Product[]
            {
            };

            Assert.IsFalse(emptyProducts.JoeyAny());
        }

        [Test]
        public void price_than_500()
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


            Assert.IsTrue(products.JoeyAny(current => current.Price > 500));
        }
    }
}