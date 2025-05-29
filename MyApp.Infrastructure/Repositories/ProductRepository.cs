using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Helpers;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PaginatedResult<Product>> GetAllAsync(int page, int size, string? q, string? sort)
        {
            var query = _db.Products.AsNoTracking();
            
            var uniqueCount = await query
                        .GroupBy(p => p.ProductName)
                        .CountAsync();

            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p => EF.Functions.ILike(p.ProductName, $"%{q}%"));

            query = sort switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_asc" => query.OrderBy(p => p.ProductName),
                "name_desc" => query.OrderByDescending(p => p.ProductName),
                _ => query.OrderBy(p => p.ProductId)
            };

            var total = await query.CountAsync();       

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();


            return new PaginatedResult<Product>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size,
                UniqueCount = uniqueCount
            };
        }

        public Task<Product?> GetByIdAsync(string id)
            => _db.Products
                  .AsNoTracking()
                  .FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task<Product> CreateAsync(CreateProductDto dto)
        {
            var prod = new Product(dto.ProductName, dto.Slug, dto.ProductImage, dto.Price,
                                   dto.DiscountStart ?? DateTime.UtcNow,
                                   dto.DiscountEnd ?? DateTime.UtcNow);
            await _db.Products.AddAsync(prod);
            await _db.SaveChangesAsync();
            return prod;
        }
    }
}
