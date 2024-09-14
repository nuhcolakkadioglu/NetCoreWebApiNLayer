using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories
{
    public class CategoryRepository(AppDbContext appDbContext) : GenericRepository<Category, int>(appDbContext), ICategoryRepository
    {
        public IQueryable<Category> GetCategoryWithProducts()
           => Context.Categories
                 .Include(c => c.Products)
                 .AsQueryable();


        public Task<Category?> GetCategoryWithProductsAsync(int id)
         => Context.Categories.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id);

        public Task<List<Category>> GetCategoryWithProductsAsync()
         => Context.Categories
                 .Include(c => c.Products)
                 .ToListAsync();
    }
}
