using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceFPE.Data;
using ECommerceFPE.Models;

namespace ECommerceFPE.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ReviewAllController : Controller
    {
        private readonly ECommerceDBContext _context;

        public ReviewAllController(ECommerceDBContext context)
        {
            _context = context;
        }

        // GET: Administrator/ReviewAll
        public async Task<IActionResult> Index()
        {
            //var eCommerceDBContext = _context.ReviewAll.Include(r => r.Customer);
            //return View(await eCommerceDBContext.ToListAsync());

            var reviews = _context.ReviewAll.Include(r => r.ApplicationUser).ToList();
            return View(reviews);
        }
        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewBag.GetSearch = search;
            var ReviewAllQuery = from r in _context.ReviewAll select r;
            if (!string.IsNullOrEmpty(search))
            {
                ReviewAllQuery = ReviewAllQuery.Where(r => r.ApplicationUser.FirstName.Contains(search));
            }
            return View(ReviewAllQuery);
        }

        // GET: Administrator/ReviewAll/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReviewAll == null)
            {
                return NotFound();
            }

            var reviewAll = await _context.ReviewAll
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviewAll == null)
            {
                return NotFound();
            }

            return View(reviewAll);
        }



        // POST: Administrator/ReviewAll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReviewAll == null)
            {
                return Problem("Entity set 'ECommerceDBContext.ReviewAll'  is null.");
            }
            var reviewAll = await _context.ReviewAll.FindAsync(id);
            if (reviewAll != null)
            {
                _context.ReviewAll.Remove(reviewAll);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult ToggleStatus(int id)
        {
            var review = _context.ReviewAll.Find(id);

            if (review != null)
            {
                review.isActive = !review.isActive;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private bool ReviewAllExists(int id)
        {
            return (_context.ReviewAll?.Any(e => e.ReviewId == id)).GetValueOrDefault();
        }
    }
}
