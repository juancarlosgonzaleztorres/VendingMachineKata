namespace VendingMachineBusinessLogic
{
    public class USPennyFeatures : ICoinFeatures
    {
        public decimal Value => 0.001m;

        public CoinTypes Type()
        {
            return CoinTypes.USPenny;
        }
    }
}
