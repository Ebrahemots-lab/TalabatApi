using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController(IProductService service) : BaseApiController
{
    private readonly IProductService _service = service;

    [HttpGet]
    public async Task<PagenationResponse<ProductDTO>> GetAllProducts([FromQuery] QueryParams param)
    {
        var productsResponse = await _service.GetAllProductsAsync(param);
        return productsResponse;
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


}