using Microsoft.AspNetCore.Mvc;
using QuarterlySalesApp.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace QuarterlySalesApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a birth date")]
        [PastDate(ErrorMessage = "Date of Birth must be a valid past date")]
        [Remote("CheckEmployee", "Validation", AdditionalFields = "FirstName, LastName")]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Please enter a hire date")]
        [PastDate(ErrorMessage = "Date of Hire must be a valid past date")]
        [GreaterThan("1/1/1995", ErrorMessage = "Hire date cant be before 1995")]
        [Display(Name = "Date of Hire")]
        public DateTime? DateOfHire { get; set; }

        [Display(Name = "Manager")]
        [Remote("CheckManager", "Validation", AdditionalFields = "EmployeeId")]
        [GreaterThan(0, ErrorMessage = "Please select a manager")]
        public int ManagerId { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
