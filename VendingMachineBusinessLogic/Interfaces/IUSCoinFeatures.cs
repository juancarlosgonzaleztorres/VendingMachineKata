namespace VendingMachineBusinessLogic
{
    public interface IUSCoinFeatures
    {
        decimal Value { get; }

        USCoinTypes Type();
    }
}