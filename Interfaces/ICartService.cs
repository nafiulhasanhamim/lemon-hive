using CartAPI.DTOs;

namespace CartAPI.Interfaces
{
    public interface ICartService
    {
        Task<CartListResponse> GetAllCartsAsync();
        Task<CartDetailDto> AddToCartAsync(AddToCartDto addToCartDto);

    }
}
