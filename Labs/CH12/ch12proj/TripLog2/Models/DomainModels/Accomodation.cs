using System.ComponentModel.DataAnnotations;

namespace TripLog2.Models.DomainModels
{
    public class Accomodation
    {
        public Accomodation() => Trips = new HashSet<Trip>();

        public int AccomodationId { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public bool HasPhone => !string.IsNullOrEmpty(Phone);
        public bool HasEmail => !string.IsNullOrEmpty(Email);
    }
}
