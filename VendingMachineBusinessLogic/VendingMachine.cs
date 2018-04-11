using System.Collections.Generic;

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
        Dictionary<USCoinTypes, int> coinReturn;

        public decimal Amount => amount;

        public string Display => coinHandler.NotEnoughChange ? Constants.EXACT_CHANGE_ONLY : display;

        public Dictionary<USCoinTypes, int> CoinReturn
        {
            get
            {
                var coins = coinReturn;
                coinReturn = null;
                return coins;
            }            
        }  

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

        public VendingMachine(IProduct product, IProductRepository productRepository)
        {

        }

        private void initialize() => display = Constants.INSERT_COIN;        

        public VendingMachine(IProductRepository repository)
        {
            productRepository = repository;
        }        
        
        public bool InsertCoin(IUSCoin usCoin)         
        {            
            if (IsValid(usCoin))
            {
                amount += usCoin.Value;
                coinHandler.AddCoins(usCoin.Type(), 1);
                display = "$"+amount.ToString();
                return true;
            }                
            else
                return false;
        }

        private bool IsValid(IUSCoin usCoin) => (usCoin.Type() == USCoinTypes.Nickel ||
                    usCoin.Type() == USCoinTypes.Dime ||
                    usCoin.Type() == USCoinTypes.Quarter) ? true : false;        

        public Product SelectProduct(ProductTypes productType)
        {
            var product = productRepository.Check(productType);
            if ( product.Inventory == 0 && lastProductChecked.Name != productType.ToString())
            {                
                display = Constants.SOLD_OUT;                
            }
            else if (product.Inventory > 0 && Amount >= product.Price)
            {
                amount -= product.Price;
                productRepository.Remove(productType);
                GetMoneyReturn();
                display = Constants.THANK_YOU;
                lastProductChecked.Name = string.Empty;
                return product;
            }                
            else if (lastProductChecked.Name != productType.ToString() || amount > 0)
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

        public void GetMoneyReturn()
        {
            coinReturn = coinHandler.GetMoneyReturned(amount);
            amount -= GetValueOfMoney(coinReturn);
            display = Constants.INSERT_COIN;
        }

        public decimal GetValueOfMoney(Dictionary<USCoinTypes, int> moneyReturned) =>
            (moneyReturned == null)?0.00m:
                     moneyReturned[USCoinTypes.Quarter] * quarterFeatures.Value 
                   + moneyReturned[USCoinTypes.Dime]    * dimeFeatures.Value 
                   + moneyReturned[USCoinTypes.Nickel]  * nickelFeatures.Value;        

        public void EmptyCoins() => coinHandler.EmptyCoins();        

        public void LoadCoins(USCoinTypes type, int number) => coinHandler.AddCoins(type, number);        
    }
}