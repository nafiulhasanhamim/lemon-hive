// MyApp.Application/Interfaces/IProductRepository.cs
using MyApp.Api.Helpers;
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<GetProductDto>> GetAllAsync(int page, int size, string? search, string? sort);
        Task<GetProductDto?> GetByIdAsync(string id);
        Task<GetProductDto> CreateAsync(CreateProductDto dto);
    }
}
