using System.ComponentModel.DataAnnotations;

namespace GenericStore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
