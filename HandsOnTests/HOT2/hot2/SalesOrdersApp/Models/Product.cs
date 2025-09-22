using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalesOrdersApp.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        public string? ProductDescShort { get; set; } = string.Empty;
        public string? ProductDescLong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product image is required.")]
        public string ProductImage { get; set; } = string.Empty;

        [Range(1, 100000, ErrorMessage = "Product price must be between 1 and 100000.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; } = 0.00m;

        [Range(1, 1000, ErrorMessage = "Product quantity must be between 1 and 1000.")]
        public int ProductQty { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }

        [NotMapped]
        public string Slug => GenerateSlug();

        private string GenerateSlug()
        {
            var s = ProductName?.ToLowerInvariant() ?? "";
            s = Regex.Replace(s, @"[^a-z0-9]+", "-").Trim('-');
            return s;
        }
    }
}