using System;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.DTOs
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
    }
    public class CreateProductDto : IValidatableObject
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(255, ErrorMessage = "Product name cannot exceed 255 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug is required.")]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductImage is required.")]
        public string? ProductImage { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        public DateTime? DiscountStart { get; set; }

        public DateTime? DiscountEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var now = DateTime.UtcNow;

            if (DiscountStart.HasValue && DiscountStart.Value < now)
            {
                yield return new ValidationResult(
                    "Discount start date cannot be in the past.",
                    new[] { nameof(DiscountStart) });
            }

            if (DiscountStart.HasValue && DiscountEnd.HasValue)
            {
                if (DiscountEnd.Value <= DiscountStart.Value)
                {
                    yield return new ValidationResult(
                        "Discount end date must be after discount start date.",
                        new[] { nameof(DiscountEnd) });
                }
            }
        }
    }
}
