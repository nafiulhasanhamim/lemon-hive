using ProductAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllProducts(int pageNumber, int pageSize, string search, string sortOrder);
        Task<GetProductDto> GetProductById(string productId);
        Task<GetProductDto> CreateProductAsync(CreateProductDto productDto);

    }
}
