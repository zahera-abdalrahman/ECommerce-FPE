using ECommerceFPE.Data;
using ECommerceFPE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceFPE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ECommerceDBContext _context;
        public HomeController(ILogger<HomeController> logger, ECommerceDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        

       
        public IActionResult Index()
        {
            ViewBag.productList = _context.Product;
            ViewBag.categoryList = _context.Category;
            return View();
        }

        public IActionResult ProductSale()
        {
            var productsWithDiscount = _context.Product.Include(p => p.Category).Where(p => p.DiscountPercent > 0).ToList();

            var discountedProducts = productsWithDiscount.Select(p => new
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId,
                Category = p.Category,
                DiscountPercent = p.DiscountPercent,
                ImageUrl = p.ImageUrl,
                DiscountedPrice = p.Price - (p.Price * p.DiscountPercent / 100)
            }).ToList();

            ViewBag.productList = discountedProducts;
            return View();
        }

        public IActionResult Category(string categoryName)
        {
            var categoryProducts = _context.Product.Where(p => p.Category.CategoryName == categoryName).ToList();
            var categoryCount = categoryProducts.Count;

            ViewBag.productList = categoryProducts;
            ViewBag.categoryList = _context.Category;
            ViewBag.categoryCount = categoryCount;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}