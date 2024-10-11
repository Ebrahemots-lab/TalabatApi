
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
public DbSet<Product> Products {get;set;}
public DbSet<Brand> Brands {get;set;}
public DbSet<ProductType> ProductTypes {get;set;}

}