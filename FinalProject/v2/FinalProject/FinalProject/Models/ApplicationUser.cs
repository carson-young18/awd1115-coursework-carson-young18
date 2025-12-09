using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Build>? Builds { get; set; }

        [NotMapped]
        public IList<string>? Roles { get; set; }
    }
}
