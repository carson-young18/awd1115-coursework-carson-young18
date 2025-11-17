using System.ComponentModel.DataAnnotations;

namespace TripLog2.Models.DomainModels
{
    public class Destination
    {
        public Destination() => Trips = new HashSet<Trip>();

        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Trip> Trips { get; set; }
    }
}
