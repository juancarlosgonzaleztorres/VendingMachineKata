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

        public decimal Value => features.Value;

        public USCoinTypes Type()
        {
            return features.Type();
        }
    }
}
