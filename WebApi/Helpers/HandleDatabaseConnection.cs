using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace WebApi.Helpers
{
    public static class HandleDatabaseConnection
    {
        public static void DatabaseConnections(this IServiceCollection service, WebApplicationBuilder builder)
        {
            service.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"), p => p.MigrationsAssembly("Api.Data"));
            });
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDB"));
            });

            builder.Services.AddScoped<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisUrl");
                return ConnectionMultiplexer.Connect(connection);
            });

        }
    }
}