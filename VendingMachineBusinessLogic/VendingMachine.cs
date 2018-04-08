using System;

namespace VendingMachineBusinessLogic
{
    public class VendingMachine:IVendingMachine
    {
        public VendingMachine()
        {
        }

        public string Display => GetDisplay();

        private string GetDisplay()
        {
            return "INSERT COIN";
        }

        public bool InsertCoin(IUSCoin usCoin)
        {
            return IsValid(usCoin);
        }

        private bool IsValid(IUSCoin usCoin)
        {
            return (usCoin.Type() == USCoinTypes.Nickel ||
                    usCoin.Type() == USCoinTypes.Dime   ||
                    usCoin.Type() == USCoinTypes.Quarter) ? true : false;
        }
    }
}