namespace VendingMachineBusinessLogic
{
    public interface ICoin
    {
        decimal Value { get; }

        CoinTypes Type();
    }
}