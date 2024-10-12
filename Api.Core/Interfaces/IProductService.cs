public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync(QueryParams param);

    Task<ProductDTO> GetProductByIdAsync(int id);

}