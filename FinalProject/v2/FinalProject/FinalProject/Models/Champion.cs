using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject.Models
{
    public class Champion
    {
        public int ChampionId { get; set; }
        public string? Name { get; set; }

        [ValidateNever]
        public ICollection<Build>? Builds { get; set; }
    }
}
