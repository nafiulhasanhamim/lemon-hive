// MyApp.Infrastructure/Repositories/CartRepository.cs
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;

        public CartRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Cart>> GetAllAsync()
            => await _db.Carts
                        .AsNoTracking()
                        .Include(c => c.Product)
                        .ToListAsync();

        public async Task<Cart?> GetByIdAsync(string id)
            => await _db.Carts
                        .AsNoTracking()
                        .Include(c => c.Product)
                        .FirstOrDefaultAsync(c => c.CartId == id);

        public async Task<Cart?> GetByProductIdAsync(string productId)
            => await _db.Carts
                        .FirstOrDefaultAsync(c => c.ProductId == productId);

        public async Task<Cart> AddAsync(Cart cart)
        {
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
            // reload with Product included
            return await GetByIdAsync(cart.CartId) ?? cart;
        }

        public async Task RemoveAsync(string cartId)
        {
            var entity = await _db.Carts.FindAsync(cartId);
            if (entity != null)
            {
                _db.Carts.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(string cartId, int newQuantity)
        {
            var entity = await _db.Carts.FindAsync(cartId);
            if (entity != null)
            {
                entity.IncreaseQuantity(newQuantity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
