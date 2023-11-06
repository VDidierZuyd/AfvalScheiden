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
    public class LogbooksController : Controller
    {
        private readonly AfvalDbContext _context;

        public LogbooksController(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: Logbooks
        public async Task<IActionResult> Index()
        {
            var afvalDbContext = _context.Logbooks.Include(l => l.Person);
            return View(await afvalDbContext.ToListAsync());
        }

        // GET: Logbooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Logbooks == null)
            {
                return NotFound();
            }

            var logbook = await _context.Logbooks
                .Include(l => l.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logbook == null)
            {
                return NotFound();
            }

            return View(logbook);
        }

        // GET: Logbooks/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name");
            return View();
        }

        // POST: Logbooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PersonId")] Logbook logbook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name", logbook.PersonId);
            return View(logbook);
        }

        // GET: Logbooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Logbooks == null)
            {
                return NotFound();
            }

            var logbook = await _context.Logbooks.FindAsync(id);
            if (logbook == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name", logbook.PersonId);
            return View(logbook);
        }

        // POST: Logbooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PersonId")] Logbook logbook)
        {
            if (id != logbook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logbook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogbookExists(logbook.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name", logbook.PersonId);
            return View(logbook);
        }

        // GET: Logbooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Logbooks == null)
            {
                return NotFound();
            }

            var logbook = await _context.Logbooks
                .Include(l => l.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logbook == null)
            {
                return NotFound();
            }

            return View(logbook);
        }

        // POST: Logbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Logbooks == null)
            {
                return Problem("Entity set 'AfvalDbContext.Logbooks'  is null.");
            }
            var logbook = await _context.Logbooks.FindAsync(id);
            if (logbook != null)
            {
                _context.Logbooks.Remove(logbook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogbookExists(int id)
        {
          return (_context.Logbooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
