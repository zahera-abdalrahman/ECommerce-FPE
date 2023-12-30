using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceFPE.Data;
using ECommerceFPE.Models;
using Microsoft.Extensions.Hosting;
using ECommerceFPE.Models.ViewModels;

namespace ECommerceFPE.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductController : Controller
    {
        private readonly ECommerceDBContext _context;

        public ProductController(ECommerceDBContext context)
        {
            _context = context;
        }

        // GET: Administrator/Product
        public async Task<IActionResult> Index()
        {
            var eCommerceDBContext = _context.Product.Include(p => p.Category);
            return View(await eCommerceDBContext.ToListAsync());
        }

        // GET: Administrator/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Administrator/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Administrator/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product, [FromServices] IWebHostEnvironment host)
            {
            string ImageName = "";
            if (product.File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "ProductImg");
                FileInfo fi = new FileInfo(product.File.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                product.File.CopyTo(new FileStream(FullPath, FileMode.Create));
            }


            var newProduct = new Product
            {
                ProductId=product.ProductId,
                ProductName=product.ProductName,
                CategoryId=product.CategoryId,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock=product.QuantityInStock,
                DiscountPercent=product.DiscountPercent,
                ImageUrl=ImageName,
        };
            _context.Add(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Administrator/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            var newProduct = new ProductModel
            {
                ProductId=product.ProductId,
                ProductName=product.ProductName,
                CategoryId=product.CategoryId,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock=product.QuantityInStock,
                DiscountPercent=product.DiscountPercent,
                ImageUrl=product.ImageUrl
            };
           
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
            return View(newProduct);
        }

        // POST: Administrator/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel product, [FromServices] IWebHostEnvironment host)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            string ImageName = "";
            if (product.File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "ProductImg");
                FileInfo fi = new FileInfo(product.File.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                product.File.CopyTo(new FileStream(FullPath, FileMode.Create));
            }
            else
            {
                ImageName = product.ImageUrl;
            }
            var newProduct = new Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                DiscountPercent = product.DiscountPercent,
                ImageUrl = ImageName,
             
            };
            _context.Update(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Administrator/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Administrator/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ECommerceDBContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
