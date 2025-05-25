using Microsoft.AspNetCore.Mvc;
using CartAPI.DTOs;
using CartAPI.Interfaces;
using dotnet_mvc.Services.Caching;
using api.Controllers;

namespace CartAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IRedisCacheService _cache;

        public CartController(ICartService cartService, IRedisCacheService cache)
        {
            _cartService = cartService;
            _cache = cache;
        }

        // Get all items in the cart (with caching)
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            string cacheKey = $"products";
            var cachedCarts = await _cache.GetDataAsync<IEnumerable<CartDetailDto>>(cacheKey);

            if (cachedCarts is not null)
            {
                Console.WriteLine("from cached carts data");
                return ApiResponse.Success(new { result = cachedCarts });
            }

            var cartList = await _cartService.GetAllCartsAsync();
            await _cache.SetDataAsync(cacheKey, cartList);

            return ApiResponse.Success(cartList, "Carts returned successfully");
        }
        // Add item to cart
        [HttpPost("create")]
        public async Task<IActionResult> AddToCartDto([FromBody] AddToCartDto addToCartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCart = await _cartService.AddToCartAsync(addToCartDto);

            await _cache.RemoveByPrefixAsync("carts");

            return ApiResponse.Created(createdCart, "Cart created");
        }
    }
}
