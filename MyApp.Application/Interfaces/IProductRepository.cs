// MyApp.Application/Interfaces/IProductRepository.cs
using MyApp.Api.Helpers;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<PaginatedResult<Product>> GetAllAsync(int page, int size, string? search, string? sort);
        Task<Product?> GetByIdAsync(string id);
        Task<Product> CreateAsync(CreateProductDto dto);
    }
}
