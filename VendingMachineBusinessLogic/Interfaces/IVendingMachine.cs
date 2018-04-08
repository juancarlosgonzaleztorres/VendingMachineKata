namespace VendingMachineBusinessLogic
{
    public interface IVendingMachine
    {
        bool InsertCoin(IUSCoin usCoin);
        string Display { get; }
    }
}