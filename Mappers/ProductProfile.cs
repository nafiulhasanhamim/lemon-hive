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
            CreateMap<Product, GetProductDto>()
                .ForMember(dest => dest.DiscountedPrice, opt => opt.MapFrom(src =>
                    (DateTime.UtcNow >= src.DiscountStart && DateTime.UtcNow < src.DiscountEnd)
                        ? src.Price * 0.75m
                        : src.Price));

            // Map CreateProductDto → Product (for creation)
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());

        }
    }
}
