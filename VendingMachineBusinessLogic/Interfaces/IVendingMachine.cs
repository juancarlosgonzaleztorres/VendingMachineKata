namespace VendingMachineBusinessLogic
{
    public interface IVendingMachine
    {
        decimal Amount { get; }
        bool InsertCoin(ICoin usCoin);
        string Display { get; }
    }
}