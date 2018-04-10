using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBusinessLogic
{
    public class MockProductRepository : IProductRepository
    {
        Dictionary<ProductTypes, Product> productsDictionary;
        public MockProductRepository()
        {
            initialize();
        }

        private void initialize()
        {
            productsDictionary = new Dictionary<ProductTypes, Product>() {
                { ProductTypes.Cola,  new Product { Price = 1.00m, Number = 1, Name = ProductTypes.Cola.ToString() } },
                { ProductTypes.Chips, new Product { Price = 0.50m, Number = 1, Name = ProductTypes.Chips.ToString() } },
                { ProductTypes.Candy, new Product { Price = 0.65m, Number = 1, Name = ProductTypes.Candy.ToString() } }
            };
        }

        public Product Check(ProductTypes productType)
        {
            var product = productsDictionary.First(p => p.Key == productType);            
            return product.Value;
        }

        public void Remove(ProductTypes productType)
        {
            var product = productsDictionary.First(p => p.Key == productType);
            if (product.Value.Number > 0)
                product.Value.Number--;            
        }
    }    
}