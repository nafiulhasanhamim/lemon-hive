// MyApp.Api/Mapping/CartProfile.cs
using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Api.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // Cart → CartDetailDto, including nested Product
            CreateMap<Cart, CartDetailDto>()
                .ForMember(d => d.Product,
                           opt => opt.MapFrom(src => src.Product));

            // AddToCartDto → Cart
            CreateMap<AddToCartDto, Cart>()
                .ConstructUsing(dto => new Cart(dto.ProductId, dto.Quantity));
        }
    }
}
