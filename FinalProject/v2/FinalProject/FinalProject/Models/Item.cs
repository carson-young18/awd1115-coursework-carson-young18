using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Cost { get; set; }

        [ValidateNever]
        public ICollection<BuildItem> BuildItems { get; set; }
    }
}