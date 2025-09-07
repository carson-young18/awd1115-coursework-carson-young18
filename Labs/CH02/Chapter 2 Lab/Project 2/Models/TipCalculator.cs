using System.ComponentModel.DataAnnotations;

namespace Project_2.Models
{
    public class TipCalculator
    {
        [Required(ErrorMessage = "Cost of meal required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost of meal must be greater than zero.")]
        public decimal? MealCost { get; set; }

        public decimal Tip15 => (MealCost ?? 0) * 0.15M;
        public decimal Tip20 => (MealCost ?? 0) * 0.20M;
        public decimal Tip25 => (MealCost ?? 0) * 0.25M;
    }
}
