using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TripLog2.Models.ViewModels
{
    public class Page1
    {
        [Required]
        public int DestinationId { get; set; }

        [Required]
        public int AccomodationId { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Accomodations { get; set; } = new List<SelectListItem>();
    }
}
