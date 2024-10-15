using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Interfaces;
using Api.Data.Repositories;
using Api.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Helpers
{
    public static class ServicesExtention
    {
        public static void AddServices(this IServiceCollection service, WebApplicationBuilder builder)
        {

            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles(builder.Configuration)));
            service.AddAuthentication().AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
                };

            });
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