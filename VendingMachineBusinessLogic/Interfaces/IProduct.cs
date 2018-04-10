namespace VendingMachineBusinessLogic
{
    public interface IProduct
    {
        string Name { get; set; }
        int Number { get; set; }
        decimal Price { get; set; }
    }
}