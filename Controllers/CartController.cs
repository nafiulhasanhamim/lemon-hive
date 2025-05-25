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
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get all items in the cart (with caching)
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var cartList = await _cartService.GetAllCartsAsync();

            return ApiResponse.Success(cartList, "Carts returned successfully");
        }
        // Add item to cart
        [HttpPost("create")]
        public async Task<IActionResult> AddToCartDto([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCart = await _cartService.AddToCartAsync(addToCartDto);
                return ApiResponse.Created(createdCart, "Cart created");
            }

            catch (Exception ex)
            {
                if (ex.Message == "Item already exists in the cart.")
                {
                    return ApiResponse.BadRequest("Item already exists in the cart.");
                }
                return ApiResponse.BadRequest(ex.Message);
            }

        }
    }
}
