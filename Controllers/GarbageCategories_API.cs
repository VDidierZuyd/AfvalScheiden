using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AfvalScheiden.Data;
using AfvalScheiden.Models;

namespace AfvalScheiden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageCategories_API : ControllerBase
    {
        private readonly AfvalDbContext _context;

        public GarbageCategories_API(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: api/GarbageCategories_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarbageCategory>>> GetGarbageCategories()
        {
          if (_context.GarbageCategories == null)
          {
              return NotFound();
          }
            return await _context.GarbageCategories.ToListAsync();
        }

        // GET: api/GarbageCategories_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GarbageCategory>> GetGarbageCategory(int id)
        {
          if (_context.GarbageCategories == null)
          {
              return NotFound();
          }
            var garbageCategory = await _context.GarbageCategories.FindAsync(id);

            if (garbageCategory == null)
            {
                return NotFound();
            }

            return garbageCategory;
        }

        // PUT: api/GarbageCategories_API/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarbageCategory(int id, GarbageCategory garbageCategory)
        {
            if (id != garbageCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(garbageCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarbageCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GarbageCategories_API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GarbageCategory>> PostGarbageCategory(GarbageCategory garbageCategory)
        {
          if (_context.GarbageCategories == null)
          {
              return Problem("Entity set 'AfvalDbContext.GarbageCategories'  is null.");
          }
            _context.GarbageCategories.Add(garbageCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarbageCategory", new { id = garbageCategory.Id }, garbageCategory);
        }

        // DELETE: api/GarbageCategories_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarbageCategory(int id)
        {
            if (_context.GarbageCategories == null)
            {
                return NotFound();
            }
            var garbageCategory = await _context.GarbageCategories.FindAsync(id);
            if (garbageCategory == null)
            {
                return NotFound();
            }

            _context.GarbageCategories.Remove(garbageCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GarbageCategoryExists(int id)
        {
            return (_context.GarbageCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
