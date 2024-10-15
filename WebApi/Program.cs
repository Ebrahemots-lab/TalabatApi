using Api.Core.Entites.Identity;
using Api.Data;
using Api.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// dotnet ef migrations add IdentityMigrations -c IdentityContext -p ..\Api.Data\ -o Migrations\Identity

builder.Services.DatabaseConnections(builder);

builder.Services.AddServices(builder);

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
})
                .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddAuthentication();



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
var context = ServicesExtention.GetService<ApplicationContext>(app);
var logger = ServicesExtention.GetService<ILogger<ExceptionHandlingMiddlware>>(app);
var identityContext = ServicesExtention.GetService<IdentityContext>(app);
var userManger = ServicesExtention.GetService<UserManager<AppUser>>(app);
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await IdentityUserSeeding.UserSeeding(userManger);
    await ApplicationSeed.SeedAsync(context);

}
catch (Exception ex)
{
    logger.LogError(ex, "There is an Error Happend");
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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();