using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Persistence
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
