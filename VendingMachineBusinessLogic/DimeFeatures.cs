using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class DimeFeatures : IUSCoinFeatures
    {
        public decimal Value => 0.10m;

        public USCoinTypes Type()
        {
            return USCoinTypes.Dime;
        }
    }
}
