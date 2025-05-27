using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Cart
    {
        public string CartId { get; private set; }

        [ForeignKey(nameof(Product))]
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Product? Product { get; private set; }

        private Cart() { }  // EF Core

        public Cart(string productId, int quantity)
        {
            CartId = Guid.NewGuid().ToString();
            ProductId = productId;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount < 1) throw new ArgumentException("Must be at least 1", nameof(amount));
            Quantity += amount;
        }
    }
}
