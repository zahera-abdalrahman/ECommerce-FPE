using ECommerceFPE.Data;
using ECommerceFPE.Models;
using ECommerceFPE.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Claims;

namespace ECommerceFPE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ECommerceDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ECommerceDBContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }



        public IActionResult Index()
        {
            ViewBag.productList = _context.Product;
            ViewBag.categoryList = _context.Category.ToList(); ;
            return View();
        }
        ////////////////////////////////////////////////////////

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
        ////////////////////////////////////////////////////////
        public IActionResult Category(string categoryName)
        {
            List<Product> categoryProducts;

            if (string.IsNullOrEmpty(categoryName))
            {
                categoryName = "All";
            }

            if (categoryName.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                categoryProducts = _context.Product.Include(p => p.Category).ToList();
            }
            else
            {
                categoryProducts = _context.Product
                    .Include(p => p.Category)
                    .Where(p => p.Category != null && p.Category.CategoryName == categoryName)
                    .ToList();
            }

            var discountedProducts = categoryProducts.Select(p => new
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
            ViewBag.categoryList = _context.Category;
            ViewBag.categoryCount = discountedProducts.Count;

            return View();
        }
        ////////////////////////////////////////////////////////
        public IActionResult ProductSingle(int productId)
        {
            var product = _context.Product
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            var discountedPrice = CalculateDiscountedPrice((decimal)product.Price, product.DiscountPercent);

            ViewBag.product = new
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                CategoryId = product.CategoryId,
                Category = product.Category,
                DiscountPercent = product.DiscountPercent,
                ImageUrl = product.ImageUrl,
                DiscountedPrice = discountedPrice
            };

            return View();
        }
        ////////////////////////////////////////////////////////
        private decimal CalculateDiscountedPrice(decimal originalPrice, decimal discountPercent)
        {
            return originalPrice - (originalPrice * discountPercent / 100);
        }

        ////////////////////////////////////////////////////////
        public async Task<IActionResult> About()
        {
            var reviews = await _context.ReviewAll
         .Include(r => r.ApplicationUser)
         .Where(r => r.isActive)
         .Take(6)
         .ToListAsync();
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.Reviews = reviews;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AboutAsync(ReviewAll model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                var newReview = new ReviewAll
                {
                    UserId = user.Id,
                    Comment = model.Comment,
                    ReviewDate = DateTime.Now,
                    isActive = false,
                    ApplicationUser = user
                };

                _context.ReviewAll.Add(newReview);
                _context.SaveChanges();

                return RedirectToAction("About");
            }
            else
            {
                ModelState.AddModelError("CustomError", "There was an issue with the submitted data.");
                return RedirectToAction("About", model);
            }
        }


        ////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        ////////////////////////////////////////////////////////

        public IActionResult AddToCart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            Cart cart = _context.Cart.FirstOrDefault(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id,
                    Total = 0
                };

                _context.Cart.Add(cart);
            }

            CartItems cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItems
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            double productPrice = GetProductPrice(productId);
            cart.Total += productPrice;

            _context.SaveChanges();

            // Pass cart data to the view using ViewBag
            ViewBag.Cart = cart;

            return View("AddToCart");
        }



        private double GetProductPrice(int productId)
        {
            return _context.Product.FirstOrDefault(p => p.ProductId == productId)?.Price ?? 0;
        }

        ////////////////////////////////////////////////////////


        public IActionResult ContactUs()
        {
            return View();
        }


    }
}

