using api.Controllers;
using dotnet_mvc.Controllers;
using dotnet_mvc.Services.Caching;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Interfaces;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRedisCacheService _cache;

        public ProductController(IProductService productService, IRedisCacheService cache)
        {
            _productService = productService;
            _cache = cache;

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 100,
            [FromQuery] string? search = null,
            [FromQuery] string? sortOrder = null)
        {

            string cacheKey = $"products:page={PageNumber}:size={PageSize}:search={search ?? "none"}:sort={sortOrder ?? "default"}";
            var cachedProducts = await _cache.GetDataAsync<PaginatedResult<GetProductDto>>(cacheKey);

            if (cachedProducts is not null)
            {
                Console.WriteLine("from cached product data");
                return ApiResponse.Success(new { result = cachedProducts });
            }

            var productList = await _productService.GetAllProducts(PageNumber, PageSize, search!, sortOrder!);
            await _cache.SetDataAsync(cacheKey, productList);

            return ApiResponse.Success(productList, "Products returned successfully");
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            string cacheKey = $"product:{productId}";
            var cachedProduct = await _cache.GetDataAsync<GetProductDto>(cacheKey);

            if (cachedProduct is not null)
            {
                Console.WriteLine("from cached product data");

                return ApiResponse.Success(cachedProduct, "Product from cache");
            }

            var product = await _productService.GetProductById(productId);
            if (product == null)
            {
                return ApiResponse.NotFound("Product with this ID not found");
            }

            await _cache.SetDataAsync(cacheKey, product);
            return ApiResponse.Success(product, "Product returned successfully");
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.CreateProductAsync(productDto);
            string cacheKey = $"product:{createdProduct.ProductId}";
            await _cache.SetDataAsync(cacheKey, createdProduct, TimeSpan.FromMinutes(10));

            await _cache.RemoveByPrefixAsync("products");

            return ApiResponse.Created(createdProduct, "Product created");
        }
    }
}
