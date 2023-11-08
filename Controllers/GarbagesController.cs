using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfvalScheiden.Data;
using AfvalScheiden.Models;

namespace AfvalScheiden.Controllers
{
    public class GarbagesController : Controller
    {
        private readonly AfvalDbContext _context;

        public GarbagesController(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: Garbages
        public async Task<IActionResult> Index()
        {
            var afvalDbContext = _context.Garbages.Include(g => g.GarbageCategory).Include(g => g.Logbook);
            return View(await afvalDbContext.ToListAsync());
        }

        // GET: Garbages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Garbages == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbages
                .Include(g => g.GarbageCategory)
                .Include(g => g.Logbook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbage == null)
            {
                return NotFound();
            }

            return View(garbage);
        }

        // GET: Garbages/Create
        public IActionResult Create()
        {
            ViewData["GarbageCategoryId"] = new SelectList(_context.GarbageCategories, "Id", "Name");
            ViewData["LogbookId"] = new SelectList(_context.Logbooks, "Id", "Name");
            return View();
        }

        // POST: Garbages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TimeNeeded,GarbageCategoryId,LogbookId")] Garbage garbage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garbage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GarbageCategoryId"] = new SelectList(_context.GarbageCategories, "Id", "Name", garbage.GarbageCategoryId);
            ViewData["LogbookId"] = new SelectList(_context.Logbooks, "Id", "Name", garbage.LogbookId);
            return View(garbage);
        }

        // GET: Garbages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Garbages == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbages.FindAsync(id);
            if (garbage == null)
            {
                return NotFound();
            }
            ViewData["GarbageCategoryId"] = new SelectList(_context.GarbageCategories, "Id", "Name", garbage.GarbageCategoryId);
            ViewData["LogbookId"] = new SelectList(_context.Logbooks, "Id", "Name", garbage.LogbookId);
            return View(garbage);
        }

        // POST: Garbages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TimeNeeded,GarbageCategoryId,LogbookId")] Garbage garbage)
        {
            if (id != garbage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garbage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarbageExists(garbage.Id))
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
            ViewData["GarbageCategoryId"] = new SelectList(_context.GarbageCategories, "Id", "Name", garbage.GarbageCategoryId);
            ViewData["LogbookId"] = new SelectList(_context.Logbooks, "Id", "Id", garbage.LogbookId);
            return View(garbage);
        }

        // GET: Garbages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Garbages == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbages
                .Include(g => g.GarbageCategory)
                .Include(g => g.Logbook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbage == null)
            {
                return NotFound();
            }

            return View(garbage);
        }

        // POST: Garbages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Garbages == null)
            {
                return Problem("Entity set 'AfvalDbContext.Garbages'  is null.");
            }
            var garbage = await _context.Garbages.FindAsync(id);
            if (garbage != null)
            {
                _context.Garbages.Remove(garbage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarbageExists(int id)
        {
          return (_context.Garbages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
