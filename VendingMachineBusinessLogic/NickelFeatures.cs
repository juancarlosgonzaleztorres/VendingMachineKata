using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class NickelFeatures : IUSCoinFeatures
    {
        public decimal Value => 0.05m;

        public USCoinTypes Type()
        {
            return USCoinTypes.Nickel;
        }
    }
}
