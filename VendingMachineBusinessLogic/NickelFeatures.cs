using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class NickelFeatures : IUSCoinFeatures
    {
        USCoinTypes IUSCoinFeatures.Type()
        {
            throw new NotImplementedException();
        }
    }
}
