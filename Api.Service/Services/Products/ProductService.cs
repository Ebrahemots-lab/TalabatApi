using AutoMapper;
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    //connect with Repository
    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(QueryParams param)
    {
        //get all products from repository
        var specs = new ProductSpecifications(param);
        var products = await _repo.GetAllWithSpecs(specs);
        //convert from Produt to ProductDTO
        var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        return mappedProducts;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var specs = new ProductSpecifications(P => P.Id == id);
        var product = await _repo.GetProductWithSepcs(specs);
        var mappedProduct = _mapper.Map<ProductDTO>(product);
        return mappedProduct;
    }
}