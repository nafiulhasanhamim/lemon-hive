using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductAPI.Models;

namespace CartAPI.Models
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Product")]
        public string? ProductId { get; set; }

        public Product? Product { get; set; } 

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
