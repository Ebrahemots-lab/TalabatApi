using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public static class ServicesExtention
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(MappingProfiles));
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}