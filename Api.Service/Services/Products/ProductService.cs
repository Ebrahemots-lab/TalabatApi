using Api.Core.Specifications;
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
    public async Task<PagenationResponse<ProductDTO>> GetAllProductsAsync(QueryParams param)
    {
        //get all products from repository
        var specs = new ProductSpecifications(param);
        var products = await _repo.GetAllWithSpecs(specs);
        //convert from Produt to ProductDTO
        var totalCountSpecification = new CountSpecifications(param);
        var totalCount = await _repo.GetCountOfItems(totalCountSpecification);
        var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        return new PagenationResponse<ProductDTO>(param.PageSize, param.PageNumber, mappedProducts, totalCount);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var specs = new ProductSpecifications(P => P.Id == id);
        var product = await _repo.GetProductWithSepcs(specs);
        var mappedProduct = _mapper.Map<ProductDTO>(product);
        return mappedProduct;
    }

    public async Task<ProductDTO> GetProductByName(string name)
    {
        var specs = new ProductSpecifications(P => P.Name == name);
        var product = await _repo.GetProductWithSepcs(specs);
        var mappedProduct = _mapper.Map<ProductDTO>(product);
        return mappedProduct;

    }
}