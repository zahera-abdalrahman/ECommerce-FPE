using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceFPE.Data;
using ECommerceFPE.Models;
using ECommerceFPE.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ECommerceFPE.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ImagesController : Controller
    {
        private readonly ECommerceDBContext _context;
        public IWebHostEnvironment Host { get; }

        public ImagesController(ECommerceDBContext context, IWebHostEnvironment Host)
        {
            _context = context;
        }

        // GET: Administrator/Images
        public async Task<IActionResult> Index()
        {
            var eCommerceDBContext = _context.ImagesProducts.Include(i => i.ProductCatalog);
            return View(await eCommerceDBContext.ToListAsync());
        }

        // GET: Administrator/Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ImagesProducts == null)
            {
                return NotFound();
            }

            var images = await _context.ImagesProducts
                .Include(i => i.ProductCatalog)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // GET: Administrator/Images/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            return View();
        }

        // POST: Administrator/Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ImagesModel images, [FromServices] IWebHostEnvironment host)
        {
            string ImageName = "", ImageName1 = "", ImageName2 = "", ImageName3 = "";
            if (images.MainImageUrlFile != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "main");
                FileInfo fi = new FileInfo(images.MainImageUrlFile.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                images.MainImageUrlFile.CopyTo(new FileStream(FullPath, FileMode.Create));
            }

            if (images.MainImageUrl1File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img1");
                FileInfo fi = new FileInfo(images.MainImageUrl1File.FileName);
                ImageName1 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath1 = Path.Combine(PathImage, ImageName1);
                images.MainImageUrl1File.CopyTo(new FileStream(FullPath1, FileMode.Create));
            }

            if (images.MainImageUrl2File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img2");
                FileInfo fi = new FileInfo(images.MainImageUrl2File.FileName);
                ImageName2 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath2 = Path.Combine(PathImage, ImageName2);
                images.MainImageUrl2File.CopyTo(new FileStream(FullPath2, FileMode.Create));
            }


            if (images.MainImageUrl3File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img3");
                FileInfo fi = new FileInfo(images.MainImageUrl3File.FileName);
                ImageName3 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath3 = Path.Combine(PathImage, ImageName3);
                images.MainImageUrl3File.CopyTo(new FileStream(FullPath3, FileMode.Create));
            }

     

            var newImages = new Images
            {
                ImageId=images.ImageId,
                ImageOrder = images.ImageOrder,
                MainImageUrl = ImageName,
                MainImageUrl1 = ImageName1,
                MainImageUrl2 = ImageName2,
                MainImageUrl3 = ImageName3,
                ProductId = images.ProductId,
                ProductCatalog = images.ProductCatalog
            };
                _context.Add(newImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Administrator/Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ImagesProducts == null)
            {
                return NotFound();
            }

            var images = await _context.ImagesProducts.FindAsync(id);
            var newImages = new ImagesModel
            {
                ImageId = images.ImageId,
                ImageOrder = images.ImageOrder,
                MainImageUrl = images.MainImageUrl,
                MainImageUrl1 = images.MainImageUrl1,
                MainImageUrl2 = images.MainImageUrl2,
                MainImageUrl3 = images.MainImageUrl3,
                ProductId = images.ProductId,
                ProductCatalog = images.ProductCatalog
            };
            if (images == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", images.ProductId);
            return View(newImages);
        }

        // POST: Administrator/Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ImagesModel images, [FromServices] IWebHostEnvironment host)
        {
            if (id != images.ImageId)
            {
                return NotFound();
            }

            string ImageName = "", ImageName1 = "", ImageName2 = "", ImageName3 = "";
            if (images.MainImageUrlFile != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "main");
                FileInfo fi = new FileInfo(images.MainImageUrlFile.FileName);
                ImageName = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath = Path.Combine(PathImage, ImageName);
                images.MainImageUrlFile.CopyTo(new FileStream(FullPath, FileMode.Create));
            }
            else
            {
                ImageName = images.MainImageUrl;
            }
            if (images.MainImageUrl1File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img1");
                FileInfo fi = new FileInfo(images.MainImageUrl1File.FileName);
                ImageName1 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath1 = Path.Combine(PathImage, ImageName1);
                images.MainImageUrl1File.CopyTo(new FileStream(FullPath1, FileMode.Create));
            }
            else
            {
                ImageName1 = images.MainImageUrl1;
            }
            if (images.MainImageUrl2File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img2");
                FileInfo fi = new FileInfo(images.MainImageUrl2File.FileName);
                ImageName2 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath2 = Path.Combine(PathImage, ImageName2);
                images.MainImageUrl2File.CopyTo(new FileStream(FullPath2, FileMode.Create));
            }
            else
            {
                ImageName2 = images.MainImageUrl2;
            }

            if (images.MainImageUrl3File != null)
            {
                string PathImage = Path.Combine(host.WebRootPath, "img3");
                FileInfo fi = new FileInfo(images.MainImageUrl3File.FileName);
                ImageName3 = "Image" + DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace("-", "").Replace(" ", "") + fi.Extension;

                string FullPath3 = Path.Combine(PathImage, ImageName3);
                images.MainImageUrl3File.CopyTo(new FileStream(FullPath3, FileMode.Create));
            }
            else
            {
                ImageName3 = images.MainImageUrl3;
            }



            var newImages = new Images
            {
                ImageId = images.ImageId,
                ImageOrder = images.ImageOrder,
                MainImageUrl = ImageName,
                MainImageUrl1 = ImageName1,
                MainImageUrl2 = ImageName2,
                MainImageUrl3 = ImageName3,
                ProductId = images.ProductId,
                ProductCatalog = images.ProductCatalog
            };
            _context.Update(newImages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Administrator/Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ImagesProducts == null)
            {
                return NotFound();
            }

            var images = await _context.ImagesProducts
                .Include(i => i.ProductCatalog)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Administrator/Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ImagesProducts == null)
            {
                return Problem("Entity set 'ECommerceDBContext.ImagesProducts'  is null.");
            }
            var images = await _context.ImagesProducts.FindAsync(id);
            if (images != null)
            {
                _context.ImagesProducts.Remove(images);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagesExists(int id)
        {
          return (_context.ImagesProducts?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
