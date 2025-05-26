// MyApp.Api/Mapping/ProductProfile.cs
using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Api.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>()
                .ForMember(d => d.DiscountedPrice,
                    opt => opt.MapFrom(src => src.GetDiscountedPrice(DateTime.UtcNow)));

            CreateMap<CreateProductDto, Product>()
                .ConstructUsing(dto => new Product(
                    dto.ProductName, dto.Slug, dto.ProductImage,
                    dto.Price, dto.DiscountStart ?? DateTime.UtcNow, dto.DiscountEnd ?? DateTime.UtcNow
                ));
        }
    }
}
