using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class AddToCartDto
    {
        [Required, MinLength(1)]
        public string ProductId { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
