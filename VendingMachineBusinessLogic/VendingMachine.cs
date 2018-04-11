using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBusinessLogic
{
    public class VendingMachine
    {
        decimal amount;
        string display;        
        IProduct lastProductChecked;
        readonly IProductRepository productRepository;
        CoinHandler coinHandler;
        static QuarterFeatures quarterFeatures;
        static DimeFeatures dimeFeatures;
        static NickelFeatures nickelFeatures;        

        public VendingMachine()
        {
            productRepository = new MockProductRepository();                        
            lastProductChecked = new Product();
            coinHandler = new CoinHandler();
            quarterFeatures = new QuarterFeatures();
            dimeFeatures = new DimeFeatures();
            nickelFeatures = new NickelFeatures();
            initialize();
        }

        private void initialize()
        {            
            display = Constants.INSERT_COIN;
        }

        public VendingMachine(IProductRepository repository)
        {
            productRepository = repository;
        }

        public string Display {
            get
            {
                if (coinHandler.NotEnoughChange)
                    display = Constants.EXACT_CHANGE_ONLY;
                return display;
            }
        }

        public decimal Amount => amount;        

        public bool InsertCoin(IUSCoin usCoin)
        {
            if (IsValid(usCoin))
            {
                amount += usCoin.Value;
                coinHandler.AddCoins(usCoin.Type(), 1);
                return true;
            }                
            else
                return false;
        }

        private bool IsValid(IUSCoin usCoin)
        {
            return (usCoin.Type() == USCoinTypes.Nickel ||
                    usCoin.Type() == USCoinTypes.Dime   ||
                    usCoin.Type() == USCoinTypes.Quarter) ? true : false;
        }

        public Product SelectProduct(ProductTypes productType)
        {
            var product = productRepository.Check(productType);
            if (product.Inventory == 0)
            {
                display = Constants.SOLD_OUT;
                return null;
            }
            else if (Amount >= product.Price)
            {
                amount -= product.Price;
                productRepository.Remove(productType);
                display = Constants.THANK_YOU;                
                return product;
            }                
            else if (lastProductChecked.Name!=productType.ToString())
            {
                display = Constants.PRICE_DISPLAY + product.Price;
            }
            else
            {
                display = Constants.INSERT_COIN;
            }
            lastProductChecked.Name = productType.ToString();
            return null;
        }

        public Dictionary<USCoinTypes, int> GetMoneyReturn()
        {
            var moneyReturned = coinHandler.GetMoneyReturned(amount);
            amount -= GetValueOfMoney(moneyReturned);
            return moneyReturned;
        }

        public decimal GetValueOfMoney(Dictionary<USCoinTypes, int> moneyReturned)
        {
            return   moneyReturned[USCoinTypes.Quarter] * quarterFeatures.Value 
                   + moneyReturned[USCoinTypes.Dime]    * dimeFeatures.Value 
                   + moneyReturned[USCoinTypes.Nickel]  * nickelFeatures.Value;
        }

        public void EmptyCoins()
        {
            display = Constants.EXACT_CHANGE_ONLY;
        }

        public void LoadCoins(USCoinTypes type, int number)
        {
            coinHandler.AddCoins(type, number);
        }
    }
}