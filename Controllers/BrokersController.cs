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
    public class BrokersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrokersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brokers
 

        public async Task<IActionResult> Index()
        {
              return _context.Broker != null ? 
                          View(await _context.Broker.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Broker'  is null.");
        }

        // GET: Brokers/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Broker == null)
            {
                return NotFound();
            }

            var broker = await _context.Broker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (broker == null)
            {
                return NotFound();
            }

            return View(broker);
        }

        // GET: Brokers/Create
        [Authorize]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Brokers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Telephone_Number,Sold_Properties")] Broker broker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(broker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(broker);
        }

        // GET: Brokers/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Broker == null)
            {
                return NotFound();
            }

            var broker = await _context.Broker.FindAsync(id);
            if (broker == null)
            {
                return NotFound();
            }
            return View(broker);
        }

        // POST: Brokers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Telephone_Number,Sold_Properties")] Broker broker)
        {
            if (id != broker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(broker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokerExists(broker.ID))
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
            return View(broker);
        }

        // GET: Brokers/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Broker == null)
            {
                return NotFound();
            }

            var broker = await _context.Broker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (broker == null)
            {
                return NotFound();
            }

            return View(broker);
        }

        // POST: Brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Broker == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Broker'  is null.");
            }
            var broker = await _context.Broker.FindAsync(id);
            if (broker != null)
            {
                _context.Broker.Remove(broker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrokerExists(int id)
        {
          return (_context.Broker?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
