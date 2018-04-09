namespace VendingMachineBusinessLogic
{
    public interface IUSCoin
    {
        decimal Value { get; }

        USCoinTypes Type();
    }
}