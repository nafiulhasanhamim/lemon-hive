// MyApp.Api/Controllers/CartController.cs

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
    public class CartsController : ControllerBase
    {
        private readonly ICartService _service;

        public CartsController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            var result = await _service.GetAllAsync(page, size);
            return ApiResponse.Success(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddToCartDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.AddToCartAsync(dto);
                return ApiResponse.Created(created);
            }
            catch (InvalidOperationException ex)
            {
                return ApiResponse.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _service.RemoveFromCartAsync(id);
            return ApiResponse.Success<object>(null, "Removed");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQty(string id, [FromQuery] int qty)
        {
            await _service.UpdateCartQuantityAsync(id, qty);
            return ApiResponse.Success<object>(null, "Quantity updated");
        }
    }
}