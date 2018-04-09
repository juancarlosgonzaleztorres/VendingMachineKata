using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineBusinessLogic;

namespace VendingMachineUnitTests
{
    [TestClass]
    public class TestVendingMachine
    {
        readonly VendingMachine vendingMachine;

        public TestVendingMachine()
        {
            vendingMachine = new VendingMachine();
        }

        [TestMethod]
        public void DisplayInsertCoin()
        {                                    
            Assert.AreEqual("INSERT COIN", vendingMachine.Display);
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
            Assert.AreEqual("PRICE $1.00", vendingMachine.Display);
        }

        [TestMethod]
        public void SelectProductsDisplaysCorrectPrice()
        {
            vendingMachine.SelectProduct(ProductTypes.Chips);
            Assert.AreEqual("PRICE $0.50", vendingMachine.Display);
            vendingMachine.SelectProduct(ProductTypes.Candy);
            Assert.AreEqual("PRICE $0.65", vendingMachine.Display);
        }
    }
}
