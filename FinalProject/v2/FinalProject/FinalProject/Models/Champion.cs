using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Champion
    {
        public int ChampionId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [ValidateNever]
        public ICollection<Build>? Builds { get; set; }
    }
}
