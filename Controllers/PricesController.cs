using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nikola_WebSite.Data;
using Nikola_WebSite.Models;

namespace Nikola_WebSite.Controllers
{
    public class PricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
              return _context.Price != null ? 
                          View(await _context.Price.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Price'  is null.");
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Price == null)
            {
                return NotFound();
            }

            var price = await _context.Price
                .FirstOrDefaultAsync(m => m.ID == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Size,Price1")] Price price)
        {
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Price == null)
            {
                return NotFound();
            }

            var price = await _context.Price.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Size,Price1")] Price price)
        {
            if (id != price.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Price == null)
            {
                return NotFound();
            }

            var price = await _context.Price
                .FirstOrDefaultAsync(m => m.ID == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Price == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Price'  is null.");
            }
            var price = await _context.Price.FindAsync(id);
            if (price != null)
            {
                _context.Price.Remove(price);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
          return (_context.Price?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
