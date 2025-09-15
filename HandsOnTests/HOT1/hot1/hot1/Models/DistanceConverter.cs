using System.ComponentModel.DataAnnotations;

namespace hot1.Models
{
    public class DistanceConverter
    {
        [Required(ErrorMessage = "Please enter a distance in inches")]
        [Range(1, 500, ErrorMessage = "Distance must be between 1 and 500 inches")]
        public decimal? Inches { get; set; }
        public decimal? Centimeters => Inches.HasValue ? Inches.Value * 2.54m : null;
    }
}
