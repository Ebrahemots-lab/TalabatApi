using Api.Data.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// dotnet ef migrations add IdentityMigrations -c IdentityContext -p ..\Api.Data\ -o Migrations\Identity

//Add Conenction To Database 
//AddDbcontext => Dependency Injection Extention
builder.Services.AddDbContext<ApplicationContext>(options =>
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

builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles(builder.Configuration)));
builder.Services.AddServices();
//validate response 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actionContext) =>
    {
        var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(P => P.ErrorMessage).ToList();

        var validationErrorResponse = new ApiValidationErrorResponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(validationErrorResponse);
    };
}
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();
var scopeProvider = scope.ServiceProvider;
var context = scopeProvider.GetRequiredService<ApplicationContext>();
var identityContext = scopeProvider.GetRequiredService<IdentityContext>();
var logger = scopeProvider.GetService<ILogger<ExceptionHandlingMiddlware>>();



try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await ApplicationSeed.SeedAsync(context);

}
catch (Exception ex)
{

}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddlware>();
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseStaticFiles();

app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();

app.Run();