namespace VendingMachineBusinessLogic
{
    public interface ICoinFeatures
    {
        decimal Value { get; }
        CoinTypes Type();
    }
}