using System.Text.Json;

public static class ApplicationSeed
{
    public async static Task SeedAsync(ApplicationContext context)
    {
        await SeedOperationOn<Brand>(context, "brands");
        await SeedOperationOn<ProductType>(context, "types");
        await SeedOperationOn<Product>(context, "products");
    }
    public async static Task SeedOperationOn<T>(ApplicationContext context, string fileName) where T : BaseEntity
    {
        if (context.Set<T>().Count() == 0)
        {
            var jsonFile = File.ReadAllText($@"..\Api.Data\DataSeeding\{fileName}.json");

            //convert from json to List<Brands>

            var listOfItems = JsonSerializer.Deserialize<List<T>>(jsonFile);


            if (listOfItems is not null)
            {
                await context.Set<T>().AddRangeAsync(listOfItems);
                //save changes
                await context.SaveChangesAsync();
            }
        }
    }
}