using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class PennyFeatures : IUSCoinFeatures
    {
        public decimal Value => 0.001m;

        public USCoinTypes Type()
        {
            return USCoinTypes.Penny;
        }
    }
}
