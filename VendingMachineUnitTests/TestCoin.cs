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
    }
}
