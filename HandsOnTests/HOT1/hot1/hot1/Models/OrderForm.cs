using System.ComponentModel.DataAnnotations;

namespace hot1.Models
{
    public class OrderForm
    {
        private const decimal TaxRate = 0.08m;

        [Required(ErrorMessage = "Please enter a quantity of shirts")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]

        public int? Quantity { get; set; }
        public decimal ShirtPrice { get; set; } = 15m;
        public string? DiscountCode { get; set; }
        public decimal Subtotal => (Quantity ?? 0) * ShirtPrice;
        public decimal DiscountAmount { get; set; }
        public decimal Tax => Subtotal * TaxRate;
        public decimal Total => Subtotal + Tax;

        public string? DiscountError { get; set; }

        public void ApplyDiscount()
        {
            DiscountAmount = 0;
            DiscountError = null;

            if(string.IsNullOrEmpty(DiscountCode))
            {
                return;
            }

            switch (DiscountCode.ToUpper())
            {
                case "6175": 
                    DiscountAmount = 0.3m;
                    DiscountError = "30% Discount Applied";
                    break;
                case "1390": 
                    DiscountAmount = 0.2m;
                    DiscountError = "20% Discount Applied";
                    break;
                case "BB88": 
                    DiscountAmount = 0.10m;
                    DiscountError = "10% Discount Applied";
                    break;
                default: 
                    DiscountError = "Invalid discount code";
                    break;
            }

            ShirtPrice -= ShirtPrice * DiscountAmount;
        }
    }
}
