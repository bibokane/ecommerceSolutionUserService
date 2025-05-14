using eCommerce.Infrastructure;
using eCommerce.Core;
using ecommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Core.Mapper;


namespace ecommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Infrastructure services
            builder.Services.AddInfrastructure();
            builder.Services.AddCore();

            // Add controllers to the service collection
            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

            //FluentValidations
            builder.Services.AddFluentValidationAutoValidation();


            //Build the web application
            var app = builder.Build();

            app.UseExceptionHandlingMiddleware();

            //Routing
            app.UseRouting();

            //Auth
            app.UseAuthentication();
            app.UseAuthorization();

            //Controller routes
            app.MapControllers();

            app.Run();
        }
    }
}
