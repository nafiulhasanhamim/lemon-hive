namespace MyApp.Application.DTOs
{
    public class CartListResponse
    {
        public IEnumerable<CartDetailDto> Items { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
