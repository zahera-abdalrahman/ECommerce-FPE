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
    public class CategoryController : Controller
    {
        private readonly ECommerceDBContext _context;

        public CategoryController(ECommerceDBContext context)
        {
            _context = context;
        }

        // GET: Administrator/ProductCategory
        public async Task<IActionResult> Index()
        {
            return _context.Category != null ?
                        View(await _context.Category.ToListAsync()) :
                        Problem("Entity set 'ECommerceDBContext.ProductCategory'  is null.");
        }
        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewBag.GetSearch = search;
            var CategoryQuery = from c in _context.Category select c;
            if (!string.IsNullOrEmpty(search))
            {
                CategoryQuery = CategoryQuery.Where(c => c.CategoryName.Contains(search));
            }
            return View(CategoryQuery.AsNoTracking().ToList());
        }

        // GET: Administrator/ProductCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var productCategory = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: Administrator/ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/ProductCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel productCategory, [FromServices] IWebHostEnvironment host)
        {
            string ImageName = "";
            if (productCategory.File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "CategoryImg");
                FileInfo fi = new FileInfo(productCategory.File.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                productCategory.File.CopyTo(new FileStream(FullPath, FileMode.Create));
            }
            var newCat = new Category
            {
                CategoryId=productCategory.CategoryId,
                CategoryName=productCategory.CategoryName,
                ImageUrl=ImageName
            };
            _context.Add(newCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Administrator/ProductCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var productCategory = await _context.Category.FindAsync(id);
            var newCat= new CategoryModel
            {
                CategoryId = productCategory.CategoryId,
                CategoryName = productCategory.CategoryName,
                ImageUrl = productCategory.ImageUrl
            };
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(newCat);
        }

        // POST: Administrator/ProductCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryModel productCategory, [FromServices] IWebHostEnvironment host)
        {
            if (id != productCategory.CategoryId)
            {
                return NotFound();
            }
            string ImageName = "";
            if (productCategory.File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "CategoryImg");
                FileInfo fi = new FileInfo(productCategory.File.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                productCategory.File.CopyTo(new FileStream(FullPath, FileMode.Create));
            }
            var newCat = new Category
            {
                CategoryId = productCategory.CategoryId,
                CategoryName = productCategory.CategoryName,
                ImageUrl = ImageName
            };
            _context.Update(newCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Administrator/ProductCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var productCategory = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: Administrator/ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'ECommerceDBContext.ProductCategory' is null.");
            }

            var productCategory = await _context.Category.FindAsync(id);
            if (productCategory != null)
            {
                productCategory.IsDeleted= true;
                await _context.SaveChangesAsync();

                // Add SweetAlert confirmation message
                TempData["SweetAlert"] = "Deleted|The category has been successfully deleted.";
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ShowSoftDeleted()
        {
            var softDeletedCategory = _context.Category.IgnoreQueryFilters().Where(p => p.IsDeleted).ToList();
            return View(softDeletedCategory);
        }
        private bool ProductCategoryExists(int id)
        {
            return (_context.Category?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}