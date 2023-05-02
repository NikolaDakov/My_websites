using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nikola_WebSite.Data;
using Nikola_WebSite.Models;

namespace Nikola_WebSite.Controllers
{
    public class text_classController : Controller
    {
        private readonly ApplicationDbContext _context;

        public text_classController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: text_class
        public async Task<IActionResult> Index()
        {
              return _context.text_class != null ? 
                          View(await _context.text_class.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.text_class'  is null.");
        }

        // GET: text_class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.text_class == null)
            {
                return NotFound();
            }

            var text_class = await _context.text_class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (text_class == null)
            {
                return NotFound();
            }

            return View(text_class);
        }

        // GET: text_class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: text_class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("text,Id,text1")] text_class text_class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(text_class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(text_class);
        }

        // GET: text_class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.text_class == null)
            {
                return NotFound();
            }

            var text_class = await _context.text_class.FindAsync(id);
            if (text_class == null)
            {
                return NotFound();
            }
            return View(text_class);
        }

        // POST: text_class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("text,Id,text1")] text_class text_class)
        {
            if (id != text_class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(text_class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!text_classExists(text_class.Id))
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
            return View(text_class);
        }

        // GET: text_class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.text_class == null)
            {
                return NotFound();
            }

            var text_class = await _context.text_class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (text_class == null)
            {
                return NotFound();
            }

            return View(text_class);
        }

        // POST: text_class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.text_class == null)
            {
                return Problem("Entity set 'ApplicationDbContext.text_class'  is null.");
            }
            var text_class = await _context.text_class.FindAsync(id);
            if (text_class != null)
            {
                _context.text_class.Remove(text_class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool text_classExists(int id)
        {
          return (_context.text_class?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
