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
        readonly DimeFeatures dimeFeatures;
        readonly NickelFeatures nickelFeatures;
        readonly PennyFeatures pennyFeatures;
        readonly USCoin quarter, dime, nickel, penny;

        public TestVendingMachine()
        {
            vendingMachine = new VendingMachine();
            quarterFeatures = new QuarterFeatures();
            dimeFeatures = new DimeFeatures();
            nickelFeatures = new NickelFeatures();
            pennyFeatures = new PennyFeatures();
            quarter = new USCoin(quarterFeatures);
            dime = new USCoin(dimeFeatures);
            nickel = new USCoin(nickelFeatures);
            penny = new USCoin(pennyFeatures);
            vendingMachine.LoadCoins(USCoinTypes.Nickel, 3);
        }

        [TestMethod]
        public void DisplayInsertCoin()
        {                                    
            Assert.AreEqual(Constants.INSERT_COIN, vendingMachine.Display);
        }       

        [TestMethod]
        public void CoinIsValidReturnsTrue()
        {            
            Assert.AreEqual(true, vendingMachine.InsertCoin(quarter));
        }

        [TestMethod]
        public void CoinIsNotValidReturnsFalse()
        {            
            Assert.AreEqual(false, vendingMachine.InsertCoin(penny));
        }

        [TestMethod]
        public void DimeAndNickelReturnValidCoin()
        {            
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
        public void GetColaProductAndCheckThankYou()
        {            
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var productSelected = vendingMachine.SelectProduct(ProductTypes.Cola);

            Assert.AreEqual(ProductTypes.Cola.ToString(), productSelected.Name);
            Assert.AreEqual(Constants.THANK_YOU, vendingMachine.Display);
        }

        [TestMethod]
        public void NotEnoughMoneyDenyColaProduct()
        {            
            vendingMachine.InsertCoin(quarter);
            
            var productSelected = vendingMachine.SelectProduct(ProductTypes.Cola);

            Assert.AreEqual(null, productSelected);
            Assert.AreEqual(PRICE_1, vendingMachine.Display);
        }

        [TestMethod]
        public void ReturnChangeAfterBuying()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var productSelected = vendingMachine.SelectProduct(ProductTypes.Candy);
            var coinsReturned = vendingMachine.GetMoneyReturn();
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);
            Assert.AreEqual(0.10m, valueOfMoney);
        }

        [TestMethod]
        public void ReturnMoney()
        {            
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(dime);
            vendingMachine.InsertCoin(nickel);
            var coinsReturned = vendingMachine.GetMoneyReturn();
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(0.40m, valueOfMoney);
        }

        [TestMethod]
        public void SoldOutDisplay()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);            

            vendingMachine.SelectProduct(ProductTypes.Chips);
            vendingMachine.SelectProduct(ProductTypes.Chips);

            Assert.AreEqual(Constants.SOLD_OUT, vendingMachine.Display);
        }

        [TestMethod]
        public void ExactChangeDisplay()
        {            
            vendingMachine.EmptyCoins();

            Assert.AreEqual(Constants.EXACT_CHANGE_ONLY, vendingMachine.Display);
        }        

        [TestMethod]
        public void Insert65CentsAndSelectChipsGivesChange()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(dime);
            vendingMachine.InsertCoin(nickel);

            vendingMachine.SelectProduct(ProductTypes.Chips);
            var coinsReturned = vendingMachine.GetMoneyReturn();
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(.15m, valueOfMoney);
        }
    }
}
