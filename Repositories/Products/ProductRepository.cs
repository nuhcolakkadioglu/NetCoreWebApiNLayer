using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Products
{
    public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product,int>(appDbContext), IProductRepository
    {

        public Task<List<Product>> GetTopPriceProductsAsyn(int count)
        {
            return Context.Products
                .OrderByDescending(m => m.Price)
                .Take(count)
                .ToListAsync();
        }
    }
}
