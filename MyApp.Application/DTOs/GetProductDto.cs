namespace MyApp.Application.DTOs
{
    public class GetProductDto
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Slug { get; set; }
        public string? ProductImage { get; set; }
        public decimal Price { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}