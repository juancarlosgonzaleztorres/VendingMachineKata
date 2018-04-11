namespace VendingMachineBusinessLogic
{
    public interface IProduct
    {
        string Name { get; set; }
        int Inventory { get; set; }
        decimal Price { get; set; }
    }
}