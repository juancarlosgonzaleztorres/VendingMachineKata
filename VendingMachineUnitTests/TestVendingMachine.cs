using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineBusinessLogic;

namespace VendingMachineUnitTests
{
    [TestClass]
    public class TestVendingMachine
    {
        private const string PRICE_1 = "PRICE $1.00";
        private const string PRICE_DOT_5 = "PRICE $0.50";
        private const string PRICE_DOT_65 = "PRICE $0.65";
        readonly VendingMachine vendingMachine;
        readonly QuarterFeatures quarterFeatures;

        public TestVendingMachine()
        {
            vendingMachine = new VendingMachine();
            quarterFeatures = new QuarterFeatures();
        }

        [TestMethod]
        public void DisplayInsertCoin()
        {                                    
            Assert.AreEqual(Constants.INSERT_COIN, vendingMachine.Display);
        }       

        [TestMethod]
        public void CoinIsValidReturnsTrue()
        {
            var usCoin = new USCoin(new QuarterFeatures());            
            Assert.AreEqual(true, vendingMachine.InsertCoin(usCoin));
        }

        [TestMethod]
        public void CoinIsNotValidReturnsFalse()
        {
            var usCoin = new USCoin(new PennyFeatures());
            Assert.AreEqual(false, vendingMachine.InsertCoin(usCoin));
        }

        [TestMethod]
        public void DimeAndNickelReturnValidCoin()
        {
            var dime = new USCoin(new DimeFeatures());
            var nickel = new USCoin(new NickelFeatures());
            Assert.AreEqual(true, vendingMachine.InsertCoin(dime));
            Assert.AreEqual(true, vendingMachine.InsertCoin(nickel));
        }

        [TestMethod]
        public void SelectProductColaDisplaysPrice()
        {            
            vendingMachine.SelectProduct(ProductTypes.Cola);
            Assert.AreEqual(PRICE_1, vendingMachine.Display);
        }

        [TestMethod]
        public void SelectProductsDisplaysCorrectPrice()
        {
            vendingMachine.SelectProduct(ProductTypes.Chips);

            Assert.AreEqual(PRICE_DOT_5, vendingMachine.Display);
            vendingMachine.SelectProduct(ProductTypes.Candy);
            Assert.AreEqual(PRICE_DOT_65, vendingMachine.Display);
        }

        [TestMethod]
        public void CheckInsertCoinDisplaysInSubsequentSelection()
        {
            vendingMachine.SelectProduct(ProductTypes.Chips);

            Assert.AreEqual(PRICE_DOT_5, vendingMachine.Display);
            vendingMachine.SelectProduct(ProductTypes.Chips);
            Assert.AreEqual(Constants.INSERT_COIN, vendingMachine.Display);
        }

        [TestMethod]
        public void GetColaProduct()
        {
            var quarterFeatures = new QuarterFeatures();
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));

            var productSelected = vendingMachine.SelectProduct(ProductTypes.Cola);

            Assert.AreEqual(ProductTypes.Cola.ToString(), productSelected.Name);
            Assert.AreEqual(Constants.THANK_YOU, vendingMachine.Display);
        }

        [TestMethod]
        public void NotEnoughMoneyDenyColaProduct()
        {            
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));     
            
            var productSelected = vendingMachine.SelectProduct(ProductTypes.Cola);

            Assert.AreEqual(null, productSelected);
            Assert.AreEqual(PRICE_1, vendingMachine.Display);
        }

        [TestMethod]
        public void ReturnChangeAfterBuying()
        {
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));
            vendingMachine.InsertCoin(new USCoin(quarterFeatures));

            var productSelected = vendingMachine.SelectProduct(ProductTypes.Candy);

            Assert.AreEqual(0.10m, vendingMachine.GetChangeReturn());
            
        }
    }
}
