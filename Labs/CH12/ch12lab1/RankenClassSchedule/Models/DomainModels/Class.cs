using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RankenClassSchedule.Models.DomainModels
{
    public class Class
    {
        public int ClassId { get; set; }

        [StringLength(200, ErrorMessage = "Title may not exceed 200 characters.")]
        [Required(ErrorMessage = "Please enter a class title.")]
        public string Title { get; set; } = string.Empty;

        [Range(100, 500, ErrorMessage = "Class number must be between 100 and 500.")]
        [Required(ErrorMessage = "Please enter a class number.")]
        public int? Number { get; set; }

        [Display(Name = "Time")]
        [RegularExpression("^[0-9]*", ErrorMessage = "Time must be numeric.")]
        [StringLength(4, MinimumLength = 4,ErrorMessage = "Time must bde 4 characters long")]
        [Required(ErrorMessage = "Please enter the time this class meets (in military).")]
        public string MilitaryTime { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        [ValidateNever]
        public Teacher Teacher { get; set; } = null!;

        public int DayId { get; set; }
        [ValidateNever]
        public Day Day { get; set; } = null!;
    }
}
