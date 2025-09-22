using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalesOrdersApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(150)]
        public string CategoryName { get; set; } = string.Empty;

        [ValidateNever]
        public ICollection<Product>? Products { get; set; }
    }
}