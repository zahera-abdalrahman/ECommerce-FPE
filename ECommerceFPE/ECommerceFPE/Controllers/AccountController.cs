using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceFPE.Models;
using ECommerceFPE.Models.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ECommerceFPE.Data;

namespace ECommerceFPE.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        //private readonly ECommerceDBContext _context;

        //public AccountController(ECommerceDBContext context)
        //{
        //    _context = context;
        //}
        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _SignInManager, RoleManager<IdentityRole> _RoleManager)

        {
            userManager = _userManager;
            signInManager = _SignInManager;
            roleManager = _RoleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "A user with this email address already exists.");
                    return View(model);
                }

                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string adminEmail = "zaheraalakash15@gmail.com";
                    if (model.Email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!await roleManager.RoleExistsAsync("Administrator"))
                        {
                            await roleManager.CreateAsync(new IdentityRole("Administrator"));
                        }

                        await userManager.AddToRoleAsync(user, "Administrator");
                    }
                    else
                    {
                        if (!await roleManager.RoleExistsAsync("User"))
                        {
                            await roleManager.CreateAsync(new IdentityRole("User"));
                        }

                        await userManager.AddToRoleAsync(user, "User");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Login");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.Email);
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles.Contains("Administrator"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User or Password");
                }
            }

            // If there are validation errors or the login attempt was unsuccessful, stay on the login page
            return View(model);
        }






        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}