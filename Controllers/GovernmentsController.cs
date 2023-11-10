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
    public class GovernmentsController : Controller
    {
        private readonly AfvalDbContext _context;

        public GovernmentsController(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: Governments
        public async Task<IActionResult> Index()
        {
              return _context.Governments != null ? 
                          View(await _context.Governments.ToListAsync()) :
                          Problem("Entity set 'AfvalDbContext.Governments'  is null.");
        }

        // GET: Governments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Governments == null)
            {
                return NotFound();
            }

            var government = await _context.Governments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (government == null)
            {
                return NotFound();
            }

            return View(government);
        }

        // GET: Governments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Governments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PostalCode")] Government government)
        {
            if (ModelState.IsValid)
            {
                _context.Add(government);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(government);
        }

        // GET: Governments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Governments == null)
            {
                return NotFound();
            }

            var government = await _context.Governments.FindAsync(id);
            if (government == null)
            {
                return NotFound();
            }
            return View(government);
        }

        // POST: Governments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PostalCode")] Government government)
        {
            if (id != government.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(government);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GovernmentExists(government.Id))
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
            return View(government);
        }

        // GET: Governments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Governments == null)
            {
                return NotFound();
            }

            var government = await _context.Governments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (government == null)
            {
                return NotFound();
            }

            return View(government);
        }

        // POST: Governments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Governments == null)
            {
                return Problem("Entity set 'AfvalDbContext.Governments'  is null.");
            }
            var government = await _context.Governments.FindAsync(id);
            if (government != null)
            {
                _context.Governments.Remove(government);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GovernmentExists(int id)
        {
          return (_context.Governments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Types()
        {
            return View();
        }
    }
}
