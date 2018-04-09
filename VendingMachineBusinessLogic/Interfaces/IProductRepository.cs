namespace VendingMachineBusinessLogic
{
    public interface IProductRepository
    {
        Product Check(ProductTypes productType);
        void Remove(ProductTypes productType);
    }
}