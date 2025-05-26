// MyApp.Domain/Entities/Product.cs
namespace MyApp.Domain.Entities
{
    public class Product
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string Slug { get; private set; }
        public string ProductImage { get; private set; }
        public decimal Price { get; private set; }
        public DateTime DiscountStart { get; private set; }
        public DateTime DiscountEnd { get; private set; }
        protected Product()
        {
        }
        // ctor for creation
        public Product(string name, string slug, string image, decimal price,
                  DateTime discountStart, DateTime discountEnd)
        {
            ProductId = Guid.NewGuid().ToString();
            ProductName = name;
            Slug = slug;
            ProductImage = image;
            Price = price;
            DiscountStart = discountStart;
            DiscountEnd = discountEnd;
        }

        // example domain behavior
        public decimal GetDiscountedPrice(DateTime now)
            => (now >= DiscountStart && now < DiscountEnd)
                ? Price * 0.75m
                : Price;
    }
}
