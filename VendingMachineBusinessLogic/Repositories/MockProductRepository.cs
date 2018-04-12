using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBusinessLogic
{
    public class MockProductRepository : IProductRepository
    {
        Dictionary<int, Product> productsDictionary;
        public MockProductRepository()
        {
            initialize();
        }

        private void initialize()
        {
            productsDictionary = new Dictionary<int, Product>() {
                { (int)ProductTypes.Cola,  new Product { Price = 1.00m, Stock = 1, Name = ProductTypes.Cola.ToString() } },
                { (int)ProductTypes.Chips, new Product { Price = 0.50m, Stock = 1, Name = ProductTypes.Chips.ToString() } },
                { (int)ProductTypes.Candy, new Product { Price = 0.65m, Stock = 2, Name = ProductTypes.Candy.ToString() } }
            };
        }

        public Product Check(int productId)
        {
            var product = productsDictionary.First(p => p.Key == productId);            
            return product.Value;
        }

        public void Remove(int productId)
        {
            var product = productsDictionary.First(p => p.Key == productId);
            if (product.Value.Stock > 0)
                product.Value.Stock--;            
        }
    }    
}