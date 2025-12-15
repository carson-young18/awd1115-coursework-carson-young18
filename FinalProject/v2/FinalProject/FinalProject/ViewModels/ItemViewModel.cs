using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class ItemViewModel
    {
        public IEnumerable<Item>? Items { get; set; }

        public Item? Item { get; set; }
        public string? Message { get; set; }
    }
}