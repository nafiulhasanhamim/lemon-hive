namespace MyApp.Application.DTOs
{
    public class CartDetailDto
    {
        public string CartId { get; set; } = null!;
        public GetProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
