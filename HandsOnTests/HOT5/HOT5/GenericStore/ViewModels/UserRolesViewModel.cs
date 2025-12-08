using System.Collections.Generic;

namespace GenericStore.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}