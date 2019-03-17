using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyCastTests
    {
        [Test]
        public void cast_int_exception_when_ArrayList_has_string()
        {
            var arrayList = new ArrayList {1, "a", 3};

            void TestDelegate() => JoeyCast<int>(arrayList).ToList();

            Assert.Throws<JoeyCastException>(TestDelegate);
        }

        private IEnumerable<T> JoeyCast<T>(IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is T result)
                {
                    yield return result;
                }
                else
                {
                    throw new JoeyCastException();
                }
            }
        }
    }

    public class JoeyCastException : Exception
    {
    }
}