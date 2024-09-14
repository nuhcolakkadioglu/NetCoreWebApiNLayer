namespace App.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        Task<List<Product>> GetTopPriceProductsAsyn(int count);
    }
}
