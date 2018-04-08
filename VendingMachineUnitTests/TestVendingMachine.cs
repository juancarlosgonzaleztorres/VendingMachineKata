using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineBusinessLogic;

namespace VendingMachineUnitTests
{
    [TestClass]
    public class TestVendingMachine
    {
        [TestMethod]
        public void DisplayInsertCoin()
        {                        
            var vendingMachine = new VendingMachine();
            Assert.AreEqual("INSERT COIN", vendingMachine.Display);
        }
    }
}
