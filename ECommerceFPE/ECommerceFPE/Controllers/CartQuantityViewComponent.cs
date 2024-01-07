using ECommerceFPE.Data;
using ECommerceFPE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the user
                ApplicationUser user = await _userManager.GetUserAsync((ClaimsPrincipal)User);

                // Check if the user is not null
                if (user != null)
                {
                    // Retrieve the user's cart
                    Cart cart = _context.Cart.FirstOrDefault(c => c.UserId == user.Id);

                    if (cart != null)
                    {
                        // Calculate total quantity in the cart
                        var totalQuantity = _context.CartItems
                            .Where(ci => ci.CartId == cart.CartId)
                            .Sum(ci => ci.Quantity);

                        return View(totalQuantity);
                    }
                }
            }

            // Return 0 if the user is not authenticated or doesn't have a cart
            return View(0);
        }
    }
}
