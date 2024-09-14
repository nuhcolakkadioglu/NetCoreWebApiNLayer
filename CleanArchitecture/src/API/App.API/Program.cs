
using App.API.ExceptionHandler;
using App.API.Filters;
using App.Application.Extensions;
using App.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidatonFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            builder.Services.AddRepository(builder.Configuration).AddService(builder.Configuration);

            builder.Services.AddScoped(typeof(NotFoundFilter<,>));
            builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseExceptionHandler(z => { });

             if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
