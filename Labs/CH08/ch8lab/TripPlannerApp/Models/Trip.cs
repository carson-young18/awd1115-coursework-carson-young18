using System.ComponentModel.DataAnnotations;

namespace TripPlannerApp.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Accommodation { get; set; } = string.Empty;

        public string? AccommodationPhone { get; set; }
        public string? AccommodationEmail { get; set; }

        public string? Activity1 { get; set; }
        public string? Activity2 { get; set; }
        public string? Activity3 { get; set; }
    }
}