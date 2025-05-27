using AutoMapper;
using MyApp.Api.Helpers;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetProductDto>> GetAllAsync(int page, int size, string? search, string? sort)
        {
            var result = await _repo.GetAllAsync(page, size, search, sort);
            // Map Product → GetProductDto
            var dtoList = result.Items.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
            return new PaginatedResult<GetProductDto>
            {
                Items = dtoList,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<GetProductDto?> GetByIdAsync(string id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product != null ? _mapper.Map<GetProductDto>(product) : null;
        }

        public async Task<GetProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = await _repo.CreateAsync(dto);
            return _mapper.Map<GetProductDto>(product);
        }
    }
}
