namespace VendingMachineBusinessLogic
{
    public class USDimeFeatures : ICoinFeatures
    {
        public decimal Value => 0.10m;

        public CoinTypes Type()
        {
            return CoinTypes.USDime;
        }
    }
}
