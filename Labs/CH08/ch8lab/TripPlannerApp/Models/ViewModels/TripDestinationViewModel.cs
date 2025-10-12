using System.ComponentModel.DataAnnotations;

namespace TripPlannerApp.Models.ViewModels
{
    public class TripDestinationViewModel
    {
        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public string Accommodation { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}