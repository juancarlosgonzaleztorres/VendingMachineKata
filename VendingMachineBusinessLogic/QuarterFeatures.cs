using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class QuarterFeatures:IUSCoinFeatures
    {
        public decimal Value => 0.25m;

        public USCoinTypes Type()
        {
            return USCoinTypes.Quarter;
        }
    }
}