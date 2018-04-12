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
        readonly USQuarterFeatures quarterFeatures;
        readonly USDimeFeatures dimeFeatures;
        readonly USNickelFeatures nickelFeatures;
        readonly USPennyFeatures pennyFeatures;
        readonly Coin quarter, dime, nickel, penny;

        public TestVendingMachine()
        {
            vendingMachine = new VendingMachine(new USCoinHandler(), new MockProductRepository());
            quarterFeatures = new USQuarterFeatures();
            dimeFeatures = new USDimeFeatures();
            nickelFeatures = new USNickelFeatures();
            pennyFeatures = new USPennyFeatures();
            quarter = new Coin(quarterFeatures);
            dime = new Coin(dimeFeatures);
            nickel = new Coin(nickelFeatures);
            penny = new Coin(pennyFeatures);
            vendingMachine.LoadCoins(CoinTypes.USNickel, 3);            
        }

        [TestMethod]
        public void DisplayInsertCoin()
        {                                    
            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);
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
            vendingMachine.SelectProduct((int)ProductTypes.Cola);
            Assert.AreEqual(PRICE_1, vendingMachine.Display);
        }

        [TestMethod]
        public void SelectProductsDisplaysCorrectPrice()
        {
            vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(PRICE_DOT_5, vendingMachine.Display);

            vendingMachine.SelectProduct((int)ProductTypes.Candy);

            Assert.AreEqual(PRICE_DOT_65, vendingMachine.Display);
        }

        [TestMethod]
        public void CheckInsertCoinDisplaysInSubsequentSelection()
        {
            vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(PRICE_DOT_5, vendingMachine.Display);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);
        }

        [TestMethod]
        public void GetColaProductAndCheckThankYou()
        {            
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var productSelected = vendingMachine.SelectProduct((int)ProductTypes.Cola);

            Assert.AreEqual(ProductTypes.Cola.ToString(), productSelected.Name);
            Assert.AreEqual(Message.THANK_YOU, vendingMachine.Display);
        }

        [TestMethod]
        public void NotEnoughMoneyDenyColaProduct()
        {            
            vendingMachine.InsertCoin(quarter);
            
            var productSelected = vendingMachine.SelectProduct((int)ProductTypes.Cola);

            Assert.AreEqual(null, productSelected);
            Assert.AreEqual(PRICE_1, vendingMachine.Display);
        }

        [TestMethod]
        public void ReturnChangeAfterBuying()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var productSelected = vendingMachine.SelectProduct((int)ProductTypes.Candy);
            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);
            Assert.AreEqual(0.10m, valueOfMoney);
        }

        [TestMethod]
        public void ReturnMoney()
        {            
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(dime);
            vendingMachine.InsertCoin(nickel);
            vendingMachine.GetMoneyReturn();
            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(0.40m, valueOfMoney);
            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);
        }

        [TestMethod]
        public void SoldOutDisplay()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);
            vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(Message.SOLD_OUT, vendingMachine.Display);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);
            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);
        }

        [TestMethod]
        public void SoldOutDisplayAfterSeveralBuyAtempts()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);
            vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(Message.SOLD_OUT, vendingMachine.Display);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);
            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);

            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var product = vendingMachine.SelectProduct((int)ProductTypes.Chips);

            Assert.AreEqual(null, product);
            Assert.AreEqual(PRICE_DOT_5, vendingMachine.Display);
        }

        [TestMethod]
        public void ExactChangeDisplay()
        {            
            vendingMachine.EmptyCoins();

            Assert.AreEqual(Message.EXACT_CHANGE_ONLY, vendingMachine.Display);
        }        

        [TestMethod]
        public void Insert65CentsAndSelectChipsGivesChange()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(dime);
            vendingMachine.InsertCoin(nickel);

            vendingMachine.SelectProduct((int)ProductTypes.Chips);
            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(.15m, valueOfMoney);
        }
        
        [TestMethod]
        public void CustomerIgnoresExactChangeMessage()
        {
            //First person
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            var candy = vendingMachine.SelectProduct((int)ProductTypes.Candy);

            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(.10m, valueOfMoney);
            Assert.AreEqual(Message.EXACT_CHANGE_ONLY, vendingMachine.Display);

            //Second person
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            candy = vendingMachine.SelectProduct((int)ProductTypes.Candy);

            coinsReturned = vendingMachine.CoinReturn;
            valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(.05m, valueOfMoney);
        }

        [TestMethod]
        public void DisplayAmountAfterInsertingCoin()
        {
            vendingMachine.InsertCoin(quarter);

            Assert.AreEqual("$0.25", vendingMachine.Display);
        }

        [TestMethod]
        public void DisplayInsertCoinWhenAmountIs0()
        {
            vendingMachine.GetMoneyReturn();
            Assert.AreEqual(Message.INSERT_COIN, vendingMachine.Display);
        }

        [TestMethod]
        public void ReturnCoinsAfterBuyingProduct()
        {
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);
            vendingMachine.InsertCoin(quarter);

            vendingMachine.SelectProduct((int)ProductTypes.Candy);
            
            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(0.35m, valueOfMoney);
        }

        [TestMethod]
        public void CoinReturnIsNullAfterCheckingIt()
        {
            vendingMachine.InsertCoin(quarter);

            vendingMachine.GetMoneyReturn();

            var coinsReturned = vendingMachine.CoinReturn;
            var valueOfMoney = vendingMachine.GetValueOfMoney(coinsReturned);

            Assert.AreEqual(0.25m, valueOfMoney);
            Assert.AreEqual(null, vendingMachine.CoinReturn);
        }

    }
}
