using System.ComponentModel.DataAnnotations;

namespace PustokApp.ViewModel.UserVm
{
    public class LoginVm
    {
        [Required]
        public string UserNameOrEmail { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}

