namespace VendingMachineBusinessLogic
{
    public interface IVendingMachine
    {
        decimal Amount { get; }
        bool InsertCoin(IUSCoin usCoin);
        string Display { get; }
    }
}