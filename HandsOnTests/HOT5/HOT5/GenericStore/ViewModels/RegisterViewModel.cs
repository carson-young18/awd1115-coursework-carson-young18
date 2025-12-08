using System.ComponentModel.DataAnnotations;

namespace GenericStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required] 
        public string FirstName { get; set; } = null!;
        [Required] 
        public string LastName { get; set; } = null!;
        [Required] 
        public string UserName { get; set; } = null!;
        [Required, EmailAddress] 
        public string Email { get; set; } = null!;
        [Required, DataType(DataType.Password)] 
        public string Password { get; set; } = null!;
        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}