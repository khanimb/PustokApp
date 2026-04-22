using System.ComponentModel.DataAnnotations;

namespace PustokApp.Areas.Manage.ViewModel;

public class AdminLoginVm
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
