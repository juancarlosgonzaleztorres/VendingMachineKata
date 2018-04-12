namespace VendingMachineBusinessLogic
{
    public class USQuarterFeatures:ICoinFeatures
    {
        public decimal Value => 0.25m;

        public CoinTypes Type()
        {
            return CoinTypes.USQuarter;
        }
    }
}