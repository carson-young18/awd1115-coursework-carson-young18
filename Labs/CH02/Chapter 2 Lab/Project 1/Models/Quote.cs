using System.ComponentModel.DataAnnotations;

namespace Project_1.Models
{
    public class Quote
    {
        [Required(ErrorMessage = "Subtotal is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Subtotal must be greater than zero.")]
        public decimal? Subtotal { get; set; }

        [Required(ErrorMessage = "Discount percent is required.")]
        [Range(0, 100, ErrorMessage = "Discount percent must be between 0 and 100.")]
        public decimal? DiscountPercent { get; set; }

        public decimal DiscountAmount => (Subtotal ?? 0) / 100 * (DiscountPercent ?? 0);
        public decimal Total => (Subtotal ?? 0) - DiscountAmount;
    }
}
