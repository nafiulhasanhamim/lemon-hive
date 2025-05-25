using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public string? ProductId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ProductName { get; set; }

        [Required]
        public string? Slug { get; set; }
        public string? ProductImage { get; set; }

        [Required]
        public decimal Price { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set; }

    }
}
