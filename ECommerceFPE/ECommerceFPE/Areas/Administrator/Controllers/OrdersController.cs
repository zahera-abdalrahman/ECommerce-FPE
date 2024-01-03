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
    public class OrdersController : Controller
    {
        private readonly ECommerceDBContext _context;

        public OrdersController(ECommerceDBContext context)
        {
            _context = context;
        }

        // GET: Administrator/Orders
        public async Task<IActionResult> Index()
        {
            var eCommerceDBContext = _context.Order.Include(o => o.ApplicationUser)
                .ToListAsync();
            return View(await eCommerceDBContext);
        }

        // GET: Administrator/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
       

        // GET: Administrator/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Administrator/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ECommerceDBContext.Orders'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
