public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync(string sort, int? brandId, int? typeId);

    Task<ProductDTO> GetProductByIdAsync(int id);

}