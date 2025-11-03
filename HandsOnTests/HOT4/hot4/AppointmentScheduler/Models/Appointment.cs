using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Start date and time is required.")]
        [Display(Name = "Start (Date and Time)")]
        [Remote(action: "CheckStartDateTime", controller: "Validation", areaName: "/",AdditionalFields = "AppointmentId")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "End (Date and Time)")]
        public DateTime EndDateTime => StartDateTime.AddHours(1);

        [Required(ErrorMessage = "Customer is required.")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}