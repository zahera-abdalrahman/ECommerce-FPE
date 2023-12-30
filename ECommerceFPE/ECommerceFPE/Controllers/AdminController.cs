using ECommerceFPE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
=======
using System.Linq;
>>>>>>> Stashed changes

namespace ECommerceFPE.Controllers;

public class AdminController : Controller
{
    private readonly ECommerceDBContext _context;

    public AdminController(ECommerceDBContext context)
    {
        _context = context;
    }

    [Route("/Administrator/admin/cart")]
    public async Task<IActionResult> Cart()
    {
        var cartList = await _context.Cart.Include(c => c.Customer).ToListAsync();

        return View("~/Views/AdminDashboard/Cart.cshtml", cartList);
    }

    [Route("/Administrator/admin/cartitems/{cartId}")]
    public async Task<IActionResult> CartItems(int cartId)
    {
        var cartItemsList = await _context
            .CartItems
            .Where(c => c.CartId == cartId)
            .Include(c => c.Cart)
            .Include(c => c.ProductCatalog)
            .ToListAsync();

        return View("~/Views/AdminDashboard/CartItems.cshtml", cartItemsList);
    }
}
