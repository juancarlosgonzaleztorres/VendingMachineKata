using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class QuarterFeatures:IUSCoinFeatures
    {        
        public USCoinTypes Type()
        {
            return USCoinTypes.Quarter;
        }
    }
}
