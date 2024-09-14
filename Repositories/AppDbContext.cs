using App.Repositories.Categories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
