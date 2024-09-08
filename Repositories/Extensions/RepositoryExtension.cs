using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.ConnectionStrings).Get<ConnectionStringOption>();
                opt.UseSqlServer(connectionStrings!.SqlServer, sqlServerOpt =>
                {
                    sqlServerOpt.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
