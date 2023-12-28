using ECommerceFPE.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceFPE.Controllers;

public class AdminController : Controller
{
    private readonly ECommerceDBContext _context;

    public AdminController(ECommerceDBContext context)
    {
        _context = context;
    }

    [Route("/admin/cart")]
    public IActionResult Cart()
    {
        var cart = _context.Cart.ToList();

        return View("~/Views/AdminDashboard/Cart.cshtml");
    }
}
