namespace VendingMachineBusinessLogic
{
    public interface IProductRepository
    {
        Product Get(ProductTypes productType);
    }
}