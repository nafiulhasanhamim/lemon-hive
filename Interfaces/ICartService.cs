using CartAPI.DTOs;

namespace CartAPI.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDetailDto>> GetAllCartsAsync();
        Task<CartDetailDto> AddToCartAsync(AddToCartDto addToCartDto);

    }
}
