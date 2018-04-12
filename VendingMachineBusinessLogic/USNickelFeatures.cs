namespace VendingMachineBusinessLogic
{
    public class USNickelFeatures : ICoinFeatures
    {
        public decimal Value => 0.05m;

        public CoinTypes Type()
        {
            return CoinTypes.USNickel;
        }
    }
}
