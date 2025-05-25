using AutoMapper;
using CartAPI.DTOs;
using CartAPI.Models;
using ProductAPI.DTOs;

namespace CartAPI.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // Map Cart → CartDetailDto (for reading)
            CreateMap<Cart, CartDetailDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            // Map AddToCartDto → Cart (for adding new items)
            CreateMap<AddToCartDto, Cart>()
                .ForMember(dest => dest.CartId, opt => opt.Ignore());
        }
    }
}
