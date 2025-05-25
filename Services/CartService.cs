using api.Controllers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CartAPI.DTOs;
using CartAPI.Interfaces;
using CartAPI.Models;
using dotnet_mvc.Controllers;
using dotnet_mvc.data;
using dotnet_mvc.Enums;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DTOs;
using ProductAPI.Interfaces;
using ProductAPI.Models;

namespace CartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CartService(AppDbContext appDbContext, IMapper mapper
        )
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<CartListResponse> GetAllCartsAsync()
        {
            IQueryable<Cart> query = _appDbContext.Carts;

            var totalCount = await query.CountAsync();
            var items = await query
                .Include(c => c.Product)
                .ProjectTo<CartDetailDto>(_mapper.ConfigurationProvider)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return new CartListResponse
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<CartDetailDto> AddToCartAsync(AddToCartDto addToCartDto)
        {

            var existingCartItem = await _appDbContext.Carts
                    .FirstOrDefaultAsync(c => c.ProductId == addToCartDto.ProductId);

            if (existingCartItem != null)
            {
                throw new Exception("Item already exists in the cart.");
            }

            var newCart = _mapper.Map<Cart>(addToCartDto);
            newCart.CartId = Guid.NewGuid().ToString();

            await _appDbContext.Carts.AddAsync(newCart);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<CartDetailDto>(newCart);

        }

    }

}

