// MyApp.Application/Services/CartService.cs
using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;
        private readonly IMapper _mapper;

        public CartService(ICartRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CartListResponse> GetAllAsync(int page, int size)
        {
            var all = await _repo.GetAllAsync();
            var paged = all.Skip((page - 1) * size).Take(size).ToList();

            return new CartListResponse
            {
                Items = _mapper.Map<List<CartDetailDto>>(paged),
                TotalCount = all.Count()
            };
        }

        public async Task<CartDetailDto> AddToCartAsync(AddToCartDto dto)
        {
            if (await _repo.GetByProductIdAsync(dto.ProductId) != null)
                throw new InvalidOperationException("Item already exists in the cart.");

            var entity = _mapper.Map<Cart>(dto);
            var saved = await _repo.AddAsync(entity);
            return _mapper.Map<CartDetailDto>(saved);
        }

        public Task RemoveFromCartAsync(string cartId)
            => _repo.RemoveAsync(cartId);

        public Task UpdateCartQuantityAsync(string cartId, int newQuantity)
            => _repo.UpdateQuantityAsync(cartId, newQuantity);
    }
}
