// MyApp.Application/Interfaces/ICartService.cs
using MyApp.Application.DTOs;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartListResponse> GetAllAsync(int page, int size);

        Task<CartDetailDto> AddToCartAsync(AddToCartDto dto);

        /// <summary>
        /// Removes an item from the cart by its CartId.
        /// </summary>
        Task RemoveFromCartAsync(string cartId);

        /// <summary>
        /// Updates the quantity of a cart item.
        /// </summary>
        Task UpdateCartQuantityAsync(string cartId, int newQuantity);
    }
}
