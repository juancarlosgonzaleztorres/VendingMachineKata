using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class PennyFeatures : IUSCoinFeatures
    {
        public USCoinTypes Type()
        {
            return USCoinTypes.Penny;
        }
    }
}
