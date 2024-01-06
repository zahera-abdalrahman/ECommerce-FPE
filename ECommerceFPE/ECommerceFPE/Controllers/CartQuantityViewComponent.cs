using ECommerceFPE.Data;
using ECommerceFPE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceFPE.Controllers
{
    public class CartQuantityViewComponent : ViewComponent
    {
        private readonly ECommerceDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartQuantityViewComponent(ECommerceDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            Cart cart = _context.Cart.FirstOrDefault(c => c.UserId == user.Id);

            if (cart != null)
            {
                var totalQuantity = _context
                    .CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .Sum(ci => ci.Quantity);

                return View(totalQuantity);
            }

            return View(0);
        }
    }
}
