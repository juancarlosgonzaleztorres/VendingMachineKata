namespace VendingMachineBusinessLogic
{
    public interface IRepository
    {
        Product Get(ProductTypes productType);
    }
}