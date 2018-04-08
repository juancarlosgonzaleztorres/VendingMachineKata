using System;

namespace VendingMachineBusinessLogic
{

    public class USCoin:IUSCoin
    {
        IUSCoinFeatures features;
        public USCoin(IUSCoinFeatures features)
        {
            this.features = features;
        }

        public USCoinTypes Type()
        {
            return features.Type();
        }
    }
}
