using App.API.ExceptionHandler;
using App.API.Filters;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiServiceExtensions(this IServiceCollection services,IConfiguration Configuration) {

            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            services.AddRepository( Configuration).AddService(Configuration);
            services.AddScoped(typeof(NotFoundFilter<,>));
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();


            return services;
        }
    }
}
