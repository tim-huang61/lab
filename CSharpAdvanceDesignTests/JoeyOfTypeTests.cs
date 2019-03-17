using System.Collections;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            // ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator1", new ProductValidator()},
                {"validator2", new ProductPriceValidator()},
            };

//             var validators = arguments.Values.OfType<IValidator<Product>>().ToList();
            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);
            // 實際運用
//            var product = JoeyOfType<Product>(arguments.Values).Single();
//            var isValid = validators.All(v=>v.Validate(product));

            Assert.AreEqual(2, validators.Count());
        }

        private IEnumerable<TResult> JoeyOfType<TResult>(IEnumerable arguments)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is TResult c)
                {
                    yield return c;
                }
            }
        }
    }
    
    public class ProductPriceValidator : IValidator<Product>
    {
        public bool Validate(Product model)
        {
            return model.Price - model.Cost >= 0;
        }
    }
}