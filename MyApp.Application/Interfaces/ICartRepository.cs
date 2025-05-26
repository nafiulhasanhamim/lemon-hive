using MyApp.Domain.Entities;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAllAsync();
    Task<Cart?> GetByIdAsync(string id);
    Task<Cart?> GetByProductIdAsync(string productId);
    Task<Cart> AddAsync(Cart cart);
    Task RemoveAsync(string cartId);
    Task UpdateQuantityAsync(string cartId, int newQuantity);
}
