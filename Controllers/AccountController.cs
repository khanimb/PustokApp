using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokApp.Models;
using PustokApp.ViewModel.UserVm;

namespace PustokApp.Controllers
{
    public class AccountController
        (
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager
        ) : Controller
    {

        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var user = await userManager.FindByNameAsync(loginVm.UserNameOrEmail)
                ?? await userManager.FindByEmailAsync(loginVm.UserNameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or email not found");
                return View(loginVm);
            }

            if (userManager.IsInRoleAsync(user, "Admin").Result)
            {
                ModelState.AddModelError("", "Admins cannot log in here");
                return View(loginVm);
            }

            var result = await signInManager.PasswordSignInAsync(
                user,
                loginVm.Password,
                loginVm.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked. Please try again later.");
                return View(loginVm);
            }

            ModelState.AddModelError("", "Invalid username/email or password");
            return View(loginVm);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
                return View(registerVm);
            var user = await userManager.FindByNameAsync(registerVm.UserName);
            if (user != null)
            {
                ModelState.AddModelError("UserName", "This username is already taken");
                return View(registerVm);
            }
            user = new AppUser
            {
                FullName = registerVm.FullName,
                UserName = registerVm.UserName,
                Email = registerVm.Email
            };

            var result = await userManager.CreateAsync(user, registerVm.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerVm);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfileAsync()
        {
            var user = await userManager.GetUserAsync(User);
            UserProfileVm userProfileVm = new UserProfileVm
            {
                UserInfo = new UserProfileInfoVm
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email
                }
            };

            return View(userProfileVm);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(UserProfileVm userProfileVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userProfileVm);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            user.UserName = userProfileVm.UserInfo.UserName;
            user.FullName = userProfileVm.UserInfo.FullName;
            user.Email = userProfileVm.UserInfo.Email;

            if (!string.IsNullOrEmpty(userProfileVm.UserInfo.NewPassword))
            {
                if (string.IsNullOrEmpty(userProfileVm.UserInfo.CurrentPassword))
                {
                    ModelState.AddModelError("", "Current password is required to set a new password");
                    return View(userProfileVm);
                }

                if (userProfileVm.UserInfo.NewPassword != userProfileVm.UserInfo.ConfirmPassword)
                {
                    ModelState.AddModelError("", "New password and confirm password do not match");
                    return View(userProfileVm);
                }

                var changePasswordResult = await userManager.ChangePasswordAsync(
                    user,
                    userProfileVm.UserInfo.CurrentPassword,
                    userProfileVm.UserInfo.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userProfileVm);
                }
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                ModelState.AddModelError("", "Profile updated successfully");
                return View(userProfileVm);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userProfileVm);
        }
        //public async Task<IActionResult> CreateRoles()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return Content("Roles created");
        //}
    }
}

