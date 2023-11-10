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
    public class Logbooks_API : ControllerBase
    {
        private readonly AfvalDbContext _context;

        public Logbooks_API(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: api/Logbooks_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logbook>>> GetLogbooks()
        {
          if (_context.Logbooks == null)
          {
              return NotFound();
          }
            return await _context.Logbooks.ToListAsync();
        }

        // GET: api/Logbooks_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Logbook>> GetLogbook(int id)
        {
          if (_context.Logbooks == null)
          {
              return NotFound();
          }
            var logbook = await _context.Logbooks.FindAsync(id);

            if (logbook == null)
            {
                return NotFound();
            }

            return logbook;
        }

        // PUT: api/Logbooks_API/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogbook(int id, Logbook logbook)
        {
            if (id != logbook.Id)
            {
                return BadRequest();
            }

            _context.Entry(logbook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogbookExists(id))
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

        // POST: api/Logbooks_API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logbook>> PostLogbook(Logbook logbook)
        {
          if (_context.Logbooks == null)
          {
              return Problem("Entity set 'AfvalDbContext.Logbooks'  is null.");
          }
            _context.Logbooks.Add(logbook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogbook", new { id = logbook.Id }, logbook);
        }

        // DELETE: api/Logbooks_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogbook(int id)
        {
            if (_context.Logbooks == null)
            {
                return NotFound();
            }
            var logbook = await _context.Logbooks.FindAsync(id);
            if (logbook == null)
            {
                return NotFound();
            }

            _context.Logbooks.Remove(logbook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogbookExists(int id)
        {
            return (_context.Logbooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
