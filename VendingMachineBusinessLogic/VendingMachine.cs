using System.Collections.Generic;

namespace VendingMachineBusinessLogic
{
    public class VendingMachine
    {
        decimal amount;
        string display;
        int? lastProductCheckedId;
        readonly IProductRepository productRepository;
        readonly ICoinHandler coinHandler;
        Dictionary<CoinTypes, int> coinReturn;

        public decimal Amount => amount;

        public string Display => coinHandler.NotEnoughChange ? Message.EXACT_CHANGE_ONLY : display;

        public Dictionary<CoinTypes, int> CoinReturn
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
            coinHandler = new USCoinHandler();
            initialize();
        }

        public VendingMachine(ICoinHandler coinHandler, IProductRepository productRepository)
        {

        }

        private void initialize() => display = Message.INSERT_COIN;        

        public VendingMachine(IProductRepository repository)
        {
            productRepository = repository;
        }        
        
        public bool InsertCoin(ICoin coinType)
        {            
            if (IsValid(coinType))
            {
                amount += coinType.Value;
                coinHandler.InsertCoin(coinType.Type(), 1);
                display = "$"+amount.ToString();
                return true;
            }                
            else
                return false;
        }

        private bool IsValid(ICoin coin) => coinHandler.IsValid(coin);

        public Product SelectProduct(int productId)
        {
            var product = productRepository.Check(productId);
            if ( product.Stock == 0 && lastProductCheckedId != productId)
            {                
                display = Message.SOLD_OUT;                
            }
            else if (product.Stock > 0 && Amount >= product.Price)
            {
                amount -= product.Price;
                productRepository.Remove(productId);
                GetMoneyReturn();
                display = Message.THANK_YOU;
                lastProductCheckedId = null;
                return product;
            }                
            else if (lastProductCheckedId != productId || amount > 0)
            {
                display = Message.PRICE_DISPLAY + product.Price;
            }
            else
            {
                display = Message.INSERT_COIN;
            }
            lastProductCheckedId = productId;
            return null;
        }

        public void GetMoneyReturn()
        {
            coinReturn = coinHandler.GetMoneyReturned(amount);
            amount -= GetValueOfMoney(coinReturn);
            display = Message.INSERT_COIN;
        }

        public decimal GetValueOfMoney(Dictionary<CoinTypes, int> moneyReturned) =>
            coinHandler.GetValueOfMoney(moneyReturned);
        
        public void EmptyCoins() => coinHandler.EmptyCoins();        

        public void LoadCoins(CoinTypes type, int number) => coinHandler.InsertCoin(type, number);
    }
}