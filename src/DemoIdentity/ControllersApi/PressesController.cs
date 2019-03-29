using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoIdentity.Data;
using DemoIdentity.Models;

namespace DemoIdentity.ControllersApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Presses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Press>>> GetPress()
        {
            return await _context.Press.ToListAsync();
        }

        // GET: api/Presses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Press>> GetPress(int id)
        {
            var press = await _context.Press.FindAsync(id);

            if (press == null)
            {
                return NotFound();
            }

            return press;
        }

        // PUT: api/Presses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPress(int id, Press press)
        {
            if (id != press.Id)
            {
                return BadRequest();
            }

            _context.Entry(press).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PressExists(id))
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

        // POST: api/Presses
        [HttpPost]
        public async Task<ActionResult<Press>> PostPress(Press press)
        {
            _context.Press.Add(press);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPress", new { id = press.Id }, press);
        }

        // DELETE: api/Presses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Press>> DeletePress(int id)
        {
            var press = await _context.Press.FindAsync(id);
            if (press == null)
            {
                return NotFound();
            }

            _context.Press.Remove(press);
            await _context.SaveChangesAsync();

            return press;
        }

        private bool PressExists(int id)
        {
            return _context.Press.Any(e => e.Id == id);
        }
    }
}
