using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext appDbContext) : GenericRepository<Category, int>(appDbContext), ICategoryRepository
    {
        public IQueryable<Category> GetCategoryWithProducts()
        {
            return Context.Categories
                 .Include(c => c.Products)
                 .AsQueryable();
        }

        public Task<Category?> GetCategoryWithProductsAsync(int id)
      => Context.Categories.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id);


    }
}
