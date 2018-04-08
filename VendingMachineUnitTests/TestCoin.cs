using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineBusinessLogic;

namespace VendingMachineUnitTests
{
    [TestClass]
    public class TestCoin
    {
        [TestMethod]
        public void TestCoinIsValidReturnsTrue()
        {            
            var usCoin = new USCoin(new QuarterFeatures());
            var vendingMachine = new VendingMachine();
            Assert.AreEqual(true, vendingMachine.InsertCoin(usCoin));
        }

        [TestMethod]
        public void TestCoinIsNotValidReturnsFalse()
        {
            var usCoin = new USCoin(new PennyFeatures());
            var vendingMachine = new VendingMachine();
            Assert.AreEqual(false, vendingMachine.InsertCoin(usCoin));
        }

        [TestMethod]
        public void TestDimeAndNickelReturnValidCoin()
        {
            var dime = new USCoin(new DimeFeatures());
            var nickel = new USCoin(new NickelFeatures());
            var vendingMachine = new VendingMachine();
            Assert.AreEqual(true, vendingMachine.InsertCoin(dime));
            Assert.AreEqual(true, vendingMachine.InsertCoin(nickel));
        }
    }
}
