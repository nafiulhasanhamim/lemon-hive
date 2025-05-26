using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Helpers;
using MyApp.Application.Caching;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Services;
namespace MyApp.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        private readonly IRedisCacheService _cache;

        public ProductsController(IProductService svc, IRedisCacheService cache)
        {
            _svc = svc;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
         [FromQuery] int page = 1,
         [FromQuery] int size = 20,
         [FromQuery] string? q = null,
         [FromQuery] string? sort = null)
        {
            var cacheKey = $"products:{page}:{size}:{q}:{sort}";
            var cached = await _cache.GetDataAsync<PaginatedResult<GetProductDto>>(cacheKey);
            if (cached != null)
            {
                Console.WriteLine("from cache");
                // 🔥 EDGE CASE: recalc discounted price on every request
                foreach (var p in cached.Items)
                {
                    p.DiscountedPrice =
                        (DateTime.UtcNow >= p.DiscountStart && DateTime.UtcNow < p.DiscountEnd)
                            ? p.Price * 0.75m
                            : p.Price;
                }
                return ApiResponse.Success(cached);
            }

            var result = await _svc.GetAllAsync(page, size, q, sort);
            await _cache.SetDataAsync(cacheKey, result, TimeSpan.FromMinutes(10));
            return ApiResponse.Success(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cacheKey = $"product:{id}";
            var cached = await _cache.GetDataAsync<GetProductDto>(cacheKey);
            if (cached != null)
            {
                Console.WriteLine("from cache");
                // 🔥 EDGE CASE: recalc discounted price on every request
                cached.DiscountedPrice =
                    (DateTime.UtcNow >= cached.DiscountStart && DateTime.UtcNow < cached.DiscountEnd)
                        ? cached.Price * 0.75m
                        : cached.Price;

                return ApiResponse.Success(cached);
            }

            var prod = await _svc.GetByIdAsync(id);
            if (prod == null) return NotFound();
            await _cache.SetDataAsync(cacheKey, prod, TimeSpan.FromMinutes(10));
            return ApiResponse.Success(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return ApiResponse.BadRequest("Invalid data");

            var created = await _svc.CreateAsync(dto);
            await _cache.RemoveByPrefixAsync("products");      // invalidate listing
            await _cache.SetDataAsync($"product:{created.ProductId}", created);

            return ApiResponse.Created(created);
        }
    }
}