using AutoMapper;
using ProductAPI.DTOs;
using ProductAPI.Models;

namespace ProductAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Map Product → GetProductDto (for reading)
            CreateMap<Product, GetProductDto>().ReverseMap();

            // Map CreateProductDto → Product (for creation)
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()); 
           
        }
    }
}
