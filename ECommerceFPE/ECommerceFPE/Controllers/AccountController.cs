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

namespace ECommerceFPE.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region InjectionAndObject
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _SignInManager, RoleManager<IdentityRole> _RoleManager)

        {
            userManager = _userManager;
            signInManager = _SignInManager;
            roleManager = _RoleManager;
        }
        #endregion

        #region User
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
            var check = await userManager.FindByEmailAsync(model.Email); // to cheik if the user already exists
            if (check != null)
            {
                ModelState.AddModelError(string.Empty, "A user with this email address already exists.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Country = model.Country
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false); // 2 line to assign role to new user by (User) role

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

        //---------------------------------------------------------------------------------------
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
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemmberMe, false);
               
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Orders", new {area = "Administrator" });
                }

                ModelState.AddModelError("", "Invalid User or Password");
                return View(model);
            }

            return View(model);
        }
        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            // return RedirectToAction("Login", "Account" , new { area = ""});
        }
        //---------------------------------------------------------------------------------------
        #endregion

        #region Roles
        [HttpGet]
        //  [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        //   [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList", "Account");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }

            return View(model);
        }
        //---------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult RolesList()
        {

            var roles = roleManager.Roles;
            return View(roles);
        }
        //---------------------------------------------------------------------------------------
        [HttpGet]
        // [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public async Task<IActionResult> EditRole(string id)

        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleName = role.Name,
                RoleId = role.Id,
                
            };

            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }


            return View(model);
        }

        [HttpPost]
        //  [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            if (ModelState.IsValid)
            {


                var role = await roleManager.FindByIdAsync(model.RoleId);
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("RolesList");
                }
                return View(model);
            }
            return View(model);

        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        //   [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public async Task<IActionResult> DeleteRole(string id)

        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            DeleteRoleViewModel model = new DeleteRoleViewModel
            {
                RoleName = role.Name,
                RoleId = role.Id
            };
            return View(model);


        }
        [HttpPost]
        // [Authorize(Roles = "Administration")]
        [AllowAnonymous]

        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {

            if (ModelState.IsValid)
            {

                var role = await roleManager.FindByIdAsync(model.RoleId);
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("RolesList");
                }
                return View(model);
            }
            return View(model);
        }




        #endregion


        public async Task<IActionResult> UserRoles()
        {
            var users = await userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Country = user.Country;
                thisViewModel.Gender = user.Gender;
                thisViewModel.Email = user.Email;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }


        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("UserRoles");
        }


    }
}
