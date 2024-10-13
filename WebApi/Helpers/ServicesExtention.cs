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
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}