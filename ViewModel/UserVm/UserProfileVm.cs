namespace PustokApp.ViewModel.UserVm
{
    public class UserProfileVm
    {
        public UserProfileInfoVm UserInfo { get; set; } = null!;
    }

    public class UserProfileInfoVm
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
