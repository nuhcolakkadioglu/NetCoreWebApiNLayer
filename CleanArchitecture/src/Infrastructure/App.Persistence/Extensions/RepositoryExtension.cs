using App.Application.Contracts.Persistence;
using App.Persistence.Categories;
using App.Persistence.Interceptors;
using App.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                //var connectionStrings = configuration
                //.GetSection(ConnectionStringOption.ConnectionStrings).Get<ConnectionStringOption>();
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlServerOpt =>
                {
                    sqlServerOpt.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                });

                opt.AddInterceptors(new AuditDbContextInterceptor());
            });

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
