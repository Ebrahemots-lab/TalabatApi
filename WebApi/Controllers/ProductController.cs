using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ApplicationContext context;

    public ProductController(IProductService service, ApplicationContext context)
    {
        _service = service;
        this.context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDTO>> GetAllProducts(string? sort, int? brandId, int? typeId)
    {
        return await _service.GetAllProductsAsync(sort, brandId, typeId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {

        var product = await _service.GetProductByIdAsync(id);
        if (product is not null)
        {
            return Ok(product);
        }

        return NotFound(new ApiBaseError(404));
    }


    [HttpGet("servererr")]
    public ActionResult Servererr()
    {

        var product = context.Products.Find(100);
        return Ok(product.ToString());
    }
}