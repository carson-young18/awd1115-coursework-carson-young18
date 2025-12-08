namespace GenericStore.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageFileName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
