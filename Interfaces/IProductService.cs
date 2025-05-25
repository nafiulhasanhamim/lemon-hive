using dotnet_mvc.Controllers;
using ProductAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<GetProductDto>> GetAllProducts(int pageNumber, int pageSize, string search, string sortOrder);
        Task<GetProductDto?> GetProductById(string productId);
        Task<GetProductDto> CreateProductAsync(CreateProductDto productDto);

    }
}
