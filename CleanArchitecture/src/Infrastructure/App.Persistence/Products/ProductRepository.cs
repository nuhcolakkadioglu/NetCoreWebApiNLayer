using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products
{
    public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product, int>(appDbContext), IProductRepository
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
