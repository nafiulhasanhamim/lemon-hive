using AutoMapper;
using AutoMapper.QueryableExtensions;
using dotnet_mvc.Controllers;
using dotnet_mvc.data;
using dotnet_mvc.Enums;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DTOs;
using ProductAPI.Interfaces;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext appDbContext, IMapper mapper
        )
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedResult<GetProductDto>> GetAllProducts(int pageNumber, int pageSize, string? search = null, string? sortOrder = null)
        {
            IQueryable<Product> query = _appDbContext.Products;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var formattedSearch = $"%{search.Trim()}%";
                query = query.Where(c => EF.Functions.ILike(c.ProductName!, formattedSearch));
            }

            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                query = query.OrderBy(c => c.ProductName);
            }
            else
            {
                var formattedSortOrder = sortOrder.Trim().ToLower();
                if (Enum.TryParse<SortOrder>(formattedSortOrder, true, out var parsedSortOrder))
                {

                    query = parsedSortOrder switch
                    {
                        SortOrder.NameAsc => query.OrderBy(c => c.ProductName),
                        SortOrder.NameDesc => query.OrderByDescending(c => c.ProductName),
                    };
                }
            }

            var totalCount = await query.CountAsync();
            var products = await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

            var items = products.Select(p =>
               {
                   var discountedPrice = DateTime.UtcNow < p.DiscountEnd && DateTime.UtcNow >= p.DiscountStart
                   ? p.Price * 0.75m
                   : p.Price;

                   return new GetProductDto
                   {
                       ProductId = p.ProductId,
                       ProductName = p.ProductName,
                       ProductImage = p.ProductImage,
                       Slug = p.Slug,
                       Price = p.Price,
                       DiscountStart = p.DiscountStart,
                       DiscountEnd = p.DiscountEnd,
                       DiscountedPrice = discountedPrice
                   };
               }).ToList();

            return new PaginatedResult<GetProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<GetProductDto?> GetProductById(string productId)
        {
            var foundProduct = await _appDbContext.Products.AsNoTracking()
                                           .FirstOrDefaultAsync(c => c.ProductId == productId);

            if (foundProduct == null)
                return null;

            var productDto = _mapper.Map<GetProductDto>(foundProduct);

            var discountedPrice = DateTime.UtcNow < foundProduct.DiscountEnd && DateTime.UtcNow >= foundProduct.DiscountStart
                ? foundProduct.Price * 0.75m // 25% discount
                : foundProduct.Price;

            productDto.DiscountedPrice = discountedPrice;

            return productDto;
        }

        public async Task<GetProductDto> CreateProductAsync(CreateProductDto productDto)
        {

            var newProduct = _mapper.Map<Product>(productDto);
            newProduct.ProductId = Guid.NewGuid().ToString();

            await _appDbContext.Products.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<GetProductDto>(newProduct);

        }

    }

}

