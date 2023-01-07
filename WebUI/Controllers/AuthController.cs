using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels.AuthViewModels;

namespace WebUI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    //private readonly RoleManager<IdentityUser> _roleMager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registeViewModel)
    {
        if (!ModelState.IsValid) return View(registeViewModel);

        AppUser user = new AppUser
        {
            UserName = registeViewModel.Username,
            Email = registeViewModel.Email,
            Fullname = registeViewModel.Fullname,
            IsActive = true

        };
        var identityResult = await _userManager.CreateAsync(user, registeViewModel.Password);

        if (!identityResult.Succeeded)
        {
            foreach (var eror in identityResult.Errors)
            {
                ModelState.AddModelError("", eror.Description);
            }
            return View(registeViewModel);
        }

        return RedirectToAction(nameof(Login));
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.EmailOrUsername);

        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginViewModel.EmailOrUsername);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login");
                return View(loginViewModel);
            }
        }

        var loginRes = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);

        if (!loginRes.Succeeded)
        {
            ModelState.AddModelError("", "Invalid login");
            return View(loginViewModel);
        }

        if (loginRes.IsLockedOut)
        {
            ModelState.AddModelError("", "Try again few moments later");
            return View(loginViewModel);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Acount is not active");
            return View(loginViewModel);
        }

        return RedirectToAction("Index", "Home");
    }



    public async Task<IActionResult> Logout()
    {


        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        return BadRequest();
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }
    public IActionResult ResetPassword()
    {
        return View();
    }
}
