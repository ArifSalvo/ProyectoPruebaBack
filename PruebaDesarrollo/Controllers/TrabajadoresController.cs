using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDesarrollo.Models;
using PruebaDesarrollo.Transfers;

namespace PruebaDesarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: api/Trabajadores
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TrabajadoresDt>>> GetListaTrabajadores()
        {
            if (_context.Trabajadores == null)
            {
                return NotFound();
            }
            
            return await (from b in _context.Trabajadores
                          select new TrabajadoresDt()
                          {
                              Id = b.Id,
                              TipoDocumento = b.TipoDocumento,
                              NumeroDocumento = b.NumeroDocumento,
                              Nombres = b.Nombres,
                              Sexo = b.Sexo,
                              IdDepartamento = b.IdDepartamento,
                              IdDistrito = b.IdDistrito,
                              IdProvincia = b.IdProvincia,
                          }).ToListAsync();
        }

        // GET: api/Trabajadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trabajadore>> GetTrabajadore(int id)
        {
            var trabajadore = await _context.Trabajadores.FindAsync(id);

            if (trabajadore == null)
            {
                return NotFound();
            }

            return trabajadore;
        }

        // PUT: api/Trabajadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabajadore(int id, Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return BadRequest();
            }

            _context.Entry(trabajadore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajadoreExists(id))
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

        // POST: api/Trabajadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trabajadore>> PostTrabajadore(Trabajadore trabajadore)
        {
            _context.Trabajadores.Add(trabajadore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrabajadore", new { id = trabajadore.Id }, trabajadore);
        }

        // DELETE: api/Trabajadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabajadore(int id)
        {
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            _context.Trabajadores.Remove(trabajadore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrabajadoreExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}
