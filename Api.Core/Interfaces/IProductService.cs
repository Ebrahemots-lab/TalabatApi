public interface IProductService
{
    Task<PagenationResponse<ProductDTO>> GetAllProductsAsync(QueryParams param);

    Task<ProductDTO> GetProductByIdAsync(int id);

}