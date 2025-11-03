using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username must be 100 characters or fewer.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}