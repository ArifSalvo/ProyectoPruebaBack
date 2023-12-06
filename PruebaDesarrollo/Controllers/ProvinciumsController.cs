using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDesarrollo.Models;

namespace PruebaDesarrollo.Controllers
{
    [Route("api/provincias")]
    [ApiController]
    public class ProvinciumsController : ControllerBase
    {
        private readonly TrabajadoresPruebaContext _context;

        public ProvinciumsController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: api/Provinciums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincium>>> GetProvincia()
        {
            return await _context.Provincia.ToListAsync();
        }

        // GET: api/Provinciums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provincium>> GetProvincium(int id)
        {
            var provincium = await _context.Provincia.FindAsync(id);

            if (provincium == null)
            {
                return NotFound();
            }

            return provincium;
        }

        // PUT: api/Provinciums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvincium(int id, Provincium provincium)
        {
            if (id != provincium.Id)
            {
                return BadRequest();
            }

            _context.Entry(provincium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciumExists(id))
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

        // POST: api/Provinciums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provincium>> PostProvincium(Provincium provincium)
        {
            _context.Provincia.Add(provincium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvincium", new { id = provincium.Id }, provincium);
        }

        // DELETE: api/Provinciums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvincium(int id)
        {
            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }

            _context.Provincia.Remove(provincium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinciumExists(int id)
        {
            return _context.Provincia.Any(e => e.Id == id);
        }
    }
}
