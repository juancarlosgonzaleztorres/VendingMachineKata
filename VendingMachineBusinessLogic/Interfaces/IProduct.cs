namespace VendingMachineBusinessLogic
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        int Stock { get; set; }
        decimal Price { get; set; }
    }
}