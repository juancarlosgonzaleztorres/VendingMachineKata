namespace VendingMachineBusinessLogic
{
    public class VendingMachine
    {
        decimal amount;
        string display;        
        Product lastProductChecked;
        readonly IProductRepository repository;

        public VendingMachine()
        {
            repository = new MockRepository();            
            display = Constants.INSERT_COIN;
            lastProductChecked = new Product();
        }

        public VendingMachine(IProductRepository repository)
        {
            this.repository = repository;
        }

        public string Display => display;

        public decimal Amount => amount;        

        public bool InsertCoin(IUSCoin usCoin)
        {
            if (IsValid(usCoin))
            {
                amount += usCoin.Value;
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
            var product = repository.Get(productType);            
            if (Amount == product.Price)
            {
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
    }
}