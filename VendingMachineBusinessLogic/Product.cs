namespace VendingMachineBusinessLogic
{
    public class Product : IProduct
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Inventory { get; set; }
    }
}