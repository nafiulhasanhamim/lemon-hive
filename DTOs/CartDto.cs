using System.ComponentModel.DataAnnotations;
using ProductAPI.DTOs;

namespace CartAPI.DTOs
{
    public class CartDetailDto
    {
        public string CartId { get; set; } = string.Empty;
        public GetProductDto Product { get; set; } = new();
        public int Quantity { get; set; }

    }
    public class AddToCartDto
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public string ProductId { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } = 1;
    }
    public class CartListResponse
    {
        public IEnumerable<CartDetailDto> Items { get; set; } = new List<CartDetailDto>();
        public int TotalCount { get; set; }
    }


}
