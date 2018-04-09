using System;

namespace VendingMachineBusinessLogic
{
    public class VendingMachine
    {
        decimal amount;
        string display;
        readonly IRepository repository;

        public VendingMachine()
        {
            repository = new MockRepository();            
            display = "INSERT COIN";
        }

        public VendingMachine(IRepository repository)
        {
            this.repository = repository;
        }

        public string Display => display;

        public decimal Amount => amount;       

        public bool InsertCoin(IUSCoin usCoin)
        {
            return IsValid(usCoin);
        }

        private bool IsValid(IUSCoin usCoin)
        {
            return (usCoin.Type() == USCoinTypes.Nickel ||
                    usCoin.Type() == USCoinTypes.Dime   ||
                    usCoin.Type() == USCoinTypes.Quarter) ? true : false;
        }

        public Product SelectProduct(ProductTypes productType)
        {
            var product = repository.Get(productType);
            if (Amount == product.Price)
            {
                display = "THANK YOU";
                return product;
            }                
            else
            {
                display = "PRICE $" + product.Price;
            }
            return null;
        }
    }
}