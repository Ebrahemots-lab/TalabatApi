
public class ProductRepository : GenericRepository<Product> , IProductRepository
{
   public ProductRepository(ApplicationContext context) : base(context)
   {
    
   }
}