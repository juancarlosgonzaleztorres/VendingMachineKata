namespace VendingMachineBusinessLogic
{
    public class Coin:ICoin
    {
        ICoinFeatures features;
        public Coin(ICoinFeatures features)
        {
            this.features = features;
        }

        public decimal Value => features.Value;

        public CoinTypes Type()
        {
            return features.Type();
        }
    }
}
