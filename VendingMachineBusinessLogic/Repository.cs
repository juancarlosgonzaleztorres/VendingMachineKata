using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBusinessLogic
{
    public class MockRepository : IProductRepository
    {
        Dictionary<ProductTypes, decimal> productsDictionary;
        public MockRepository()
        {
            initialize();
        }

        private void initialize()
        {
            productsDictionary = new Dictionary<ProductTypes, decimal>() {
                { ProductTypes.Cola,  1.00m },
                { ProductTypes.Chips, 0.50m },
                { ProductTypes.Candy, 0.65m }
            };            
        }

        public Product Get(ProductTypes productType)
        {
            return productsDictionary.Where(p => p.Key == productType).
                Select(p=>new Product { Price = p.Value, Name = p.Key.ToString()}).First();
        }
    }
}