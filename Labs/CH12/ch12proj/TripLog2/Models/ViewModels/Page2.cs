using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TripLog2.Models.ViewModels
{
    public class Page2
    {
        [Required]
        public int DestinationId { get; set; }

        [Required]
        public int AccomodationId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public List<int> SelectedActivityIds { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Activities { get; set; } = new List<SelectListItem>();
    }
}
