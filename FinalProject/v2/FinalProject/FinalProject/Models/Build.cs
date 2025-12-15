using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FinalProject.Models
{
    public class Build
    {
        public Build()
        {
            BuildItems = new List<BuildItem>();
        }
        public int BuildId { get; set; }
        public string? UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ChampionId { get; set; }
        public int TotalCost { get; set; }
        [Required]
        public string? Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
        [ValidateNever]
        public Champion? Champion { get; set; }
        [ValidateNever]
        public ICollection<BuildItem>? BuildItems { get; set; }
    }
}