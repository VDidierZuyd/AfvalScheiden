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
    public class GarbageCategoriesController : Controller
    {
        private readonly AfvalDbContext _context;

        public GarbageCategoriesController(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: GarbageCategories
        public async Task<IActionResult> Index()
        {
              return _context.GarbageCategories != null ? 
                          View(await _context.GarbageCategories.ToListAsync()) :
                          Problem("Entity set 'AfvalDbContext.GarbageCategories'  is null.");
        }

        // GET: GarbageCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GarbageCategories == null)
            {
                return NotFound();
            }

            var garbageCategory = await _context.GarbageCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbageCategory == null)
            {
                return NotFound();
            }

            return View(garbageCategory);
        }

        // GET: GarbageCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GarbageCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GarbageCategory garbageCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garbageCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garbageCategory);
        }

        // GET: GarbageCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GarbageCategories == null)
            {
                return NotFound();
            }

            var garbageCategory = await _context.GarbageCategories.FindAsync(id);
            if (garbageCategory == null)
            {
                return NotFound();
            }
            return View(garbageCategory);
        }

        // POST: GarbageCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GarbageCategory garbageCategory)
        {
            if (id != garbageCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garbageCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarbageCategoryExists(garbageCategory.Id))
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
            return View(garbageCategory);
        }

        // GET: GarbageCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GarbageCategories == null)
            {
                return NotFound();
            }

            var garbageCategory = await _context.GarbageCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbageCategory == null)
            {
                return NotFound();
            }

            return View(garbageCategory);
        }

        // POST: GarbageCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GarbageCategories == null)
            {
                return Problem("Entity set 'AfvalDbContext.GarbageCategories'  is null.");
            }
            var garbageCategory = await _context.GarbageCategories.FindAsync(id);
            if (garbageCategory != null)
            {
                _context.GarbageCategories.Remove(garbageCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarbageCategoryExists(int id)
        {
          return (_context.GarbageCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
