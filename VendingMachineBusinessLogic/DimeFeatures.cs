using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class DimeFeatures : IUSCoinFeatures
    {
        USCoinTypes IUSCoinFeatures.Type()
        {
            throw new NotImplementedException();
        }
    }
}
