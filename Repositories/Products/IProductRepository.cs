namespace App.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetTopPriceProductsAsyn(int count);
    }
}
