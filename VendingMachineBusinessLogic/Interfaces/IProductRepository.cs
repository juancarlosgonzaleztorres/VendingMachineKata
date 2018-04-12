namespace VendingMachineBusinessLogic
{
    public interface IProductRepository
    {
        Product Check(int productId);
        void Remove(int productId);
    }
}