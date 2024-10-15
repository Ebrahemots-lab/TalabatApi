using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Interfaces;
using Api.Data.Repositories;

namespace WebApi.Helpers
{
    public static class ServicesExtention
    {
        public static void AddServices(this IServiceCollection service, WebApplicationBuilder builder)
        {

            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles(builder.Configuration)));
        }


        public static T GetService<T>(WebApplication app) where T : class
        {
            var scope = app.Services.CreateScope();
            var scopeProvider = scope.ServiceProvider;
            var service = scopeProvider.GetService<T>();
            return service;

        }
    }
}