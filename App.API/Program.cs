using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRepository(builder.Configuration).AddService(builder.Configuration);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidatonFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
