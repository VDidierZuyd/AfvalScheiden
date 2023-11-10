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
    public class Governments_API : ControllerBase
    {
        private readonly AfvalDbContext _context;

        public Governments_API(AfvalDbContext context)
        {
            _context = context;
        }

        // GET: api/Governments_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Government>>> GetGovernments()
        {
          if (_context.Governments == null)
          {
              return NotFound();
          }
            return await _context.Governments.ToListAsync();
        }

        // GET: api/Governments_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Government>> GetGovernment(int id)
        {
          if (_context.Governments == null)
          {
              return NotFound();
          }
            var government = await _context.Governments.FindAsync(id);

            if (government == null)
            {
                return NotFound();
            }

            return government;
        }

        // PUT: api/Governments_API/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGovernment(int id, Government government)
        {
            if (id != government.Id)
            {
                return BadRequest();
            }

            _context.Entry(government).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GovernmentExists(id))
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

        // POST: api/Governments_API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Government>> PostGovernment(Government government)
        {
          if (_context.Governments == null)
          {
              return Problem("Entity set 'AfvalDbContext.Governments'  is null.");
          }
            _context.Governments.Add(government);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGovernment", new { id = government.Id }, government);
        }

        // DELETE: api/Governments_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGovernment(int id)
        {
            if (_context.Governments == null)
            {
                return NotFound();
            }
            var government = await _context.Governments.FindAsync(id);
            if (government == null)
            {
                return NotFound();
            }

            _context.Governments.Remove(government);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GovernmentExists(int id)
        {
            return (_context.Governments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
