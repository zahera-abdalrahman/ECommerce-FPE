using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceFPE.Models;
using ECommerceFPE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ECommerceFPE.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class DashboardController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;


        public DashboardController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _RoleManager)

        {
            roleManager = _RoleManager;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UsersListDashboard()
        {
            var users = userManager.Users;
            return View(users);
        }


        #region Roles
        [HttpGet]
        public IActionResult RolesListDashboard()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        //  [Authorize(Roles = "Administration")]
        // [AllowAnonymous]

        public IActionResult CreateRoleDashboard()
        {
            return View();
        }
        [HttpPost]
        //   [Authorize(Roles = "Administration")]
        // [AllowAnonymous]

        public async Task<IActionResult> CreateRoleDashboard(CreateRoleViewModel model)
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
                    return RedirectToAction("RolesListDashboard");
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
        // [Authorize(Roles = "Administration")]


        public async Task<IActionResult> EditRoleDashboard(string id)

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
        //  [AllowAnonymous]

        public async Task<IActionResult> EditRoleDashboard(EditRoleViewModel model)
        {

            if (ModelState.IsValid)
            {


                var role = await roleManager.FindByIdAsync(model.RoleId);
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("RolesListDashboard");
                }
                return View(model);
            }
            return View(model);

        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        //   [Authorize(Roles = "Administration")]
        //  [AllowAnonymous]

        public async Task<IActionResult> DeleteRoleDashboard(string id)

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
        // [AllowAnonymous]

        public async Task<IActionResult> DeleteRoleDashboard(DeleteRoleViewModel model)
        {

            if (ModelState.IsValid)
            {

                var role = await roleManager.FindByIdAsync(model.RoleId);
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("RolesListDashboard");
                }
                return View(model);
            }
            return View(model);
        }




        #endregion





    }
}
