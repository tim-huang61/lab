using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
//    [Ignore("not yet")]
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

//            drawlingList.Aggregate()
            var actual = JoeyAggregate(drawlingList, balance, (seed, current) =>
            {
                if (seed >= current)
                {
                    seed -= current;
                }

                return seed;
            });

            var expected = 10.91m;

            Assert.AreEqual(expected, actual);
        }

        private decimal JoeyAggregate(IEnumerable<int> drawlingList, decimal balance, Func<decimal, int, decimal> dawling)
        {
            var seed = balance;
            var enumerator = drawlingList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                seed = dawling(seed, current);
            }

            return seed;
        }

        private static decimal Dawling(decimal seed, int current)
        {
            if (seed >= current)
            {
                seed -= current;
            }

            return seed;
        }
    }
}