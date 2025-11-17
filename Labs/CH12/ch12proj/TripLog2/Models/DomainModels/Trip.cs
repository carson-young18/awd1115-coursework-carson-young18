using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TripLog2.Models.DomainModels
{
    public class Trip
    {
        public Trip() => Activities = new HashSet<Activity>();

        public int TripId { get; set; }

        [ValidateNever]
        public int DestinationId { get; set; }

        public Destination Destination { get; set; } = null!;

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [ValidateNever]
        public Accomodation Accomodation { get; set; } = null!;

        public ICollection<Activity> Activities { get; set; }
    }
}
