using System.ComponentModel.DataAnnotations;

namespace PustokApp.ViewModel.UserVm
{
    public class RegisterVm
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        [MinLength(2)]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}

