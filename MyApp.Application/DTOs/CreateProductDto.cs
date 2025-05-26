using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class CreateProductDto : IValidatableObject
    {
        [Required, MaxLength(255)]
        public string ProductName { get; set; } = "";

        [Required]
        public string Slug { get; set; } = "";

        [Required]
        public string ProductImage { get; set; } = "";

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            var now = DateTime.UtcNow;
            if (DiscountStart < now)
                yield return new ValidationResult("Cannot start discount in the past.", new[] { nameof(DiscountStart) });
            if (DiscountEnd <= DiscountStart)
                yield return new ValidationResult("End must be after start.", new[] { nameof(DiscountEnd) });
        }
    }
}