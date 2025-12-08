using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace GenericStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        [Range(0, 10000)]
        public int StockQty { get; set; } = 0;

        [Range(0.01, 100000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string ImageFileName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public Category? Category { get; set; }

        public static string GenerateSlug(string input)
        {
            var s = (input ?? "").ToLowerInvariant();
            s = Regex.Replace(s, @"[^a-z0-9]+", "-").Trim('-');
            return s;
        }
    }
}