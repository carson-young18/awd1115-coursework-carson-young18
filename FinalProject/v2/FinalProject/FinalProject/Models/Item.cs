using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public int Cost { get; set; }

        [ValidateNever]
        public ICollection<BuildItem> BuildItems { get; set; }
    }
}