using System;

namespace VendingMachineBusinessLogic
{
    public class VendingMachine:IVendingMachine
    {
        public VendingMachine()
        {
        }

        public bool InsertCoin(IUSCoin usCoin)
        {
            if (IsValid(usCoin))
                return true;
            return false;                
        }

        private bool IsValid(IUSCoin usCoin)
        {
            return (usCoin.Type() == USCoinTypes.Nickel ||
                    usCoin.Type() == USCoinTypes.Dime   ||
                    usCoin.Type() == USCoinTypes.Quarter) ? true : false;
        }
    }
}