namespace VendingMachineBusinessLogic
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }        
    }
}