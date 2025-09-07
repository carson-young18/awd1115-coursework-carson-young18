using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class FutureValueModel
    {
        [Required(ErrorMessage = "Please enter a valid investment")]
        [Range(1, 500, ErrorMessage = "Investment must be between 1 and 500")]
        public decimal? MonthlyInvestment { get; set; }

        [Required(ErrorMessage = "Please enter a valid interest rate")]
        [Range(1, 20, ErrorMessage = "Interest rate must be between 1 and 20")]
        public decimal? YearlyInterestRate { get; set; }

        [Required(ErrorMessage = "Please enter a valid number of years")]
        [Range(1, 50, ErrorMessage = "Number of years must be between 1 and 50")]
        public int? Years { get; set; }
        public decimal? CalculateFutureValue()
        {
            int? months = Years * 12;
            decimal? monthlyInterestRate = YearlyInterestRate / 12 / 100;
            decimal? futureValue = 0;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + MonthlyInvestment) * (1 + monthlyInterestRate);
            }
            return futureValue;
        }
    }
}
