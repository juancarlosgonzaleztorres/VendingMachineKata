using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class NickelFeatures : IUSCoinFeatures
    {
        public USCoinTypes Type()
        {
            return USCoinTypes.Nickel;
        }
    }
}
