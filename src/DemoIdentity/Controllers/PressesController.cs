using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoIdentity.Data;
using DemoIdentity.Models;

namespace DemoIdentity.Controllers
{
    public class PressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Press.ToListAsync());
        }

        // GET: Presses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var press = await _context.Press
                .FirstOrDefaultAsync(m => m.Id == id);
            if (press == null)
            {
                return NotFound();
            }

            return View(press);
        }

        // GET: Presses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Category")] Press press)
        {
            if (ModelState.IsValid)
            {
                _context.Add(press);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(press);
        }

        // GET: Presses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var press = await _context.Press.FindAsync(id);
            if (press == null)
            {
                return NotFound();
            }
            return View(press);
        }

        // POST: Presses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Category")] Press press)
        {
            if (id != press.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(press);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PressExists(press.Id))
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
            return View(press);
        }

        // GET: Presses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var press = await _context.Press
                .FirstOrDefaultAsync(m => m.Id == id);
            if (press == null)
            {
                return NotFound();
            }

            return View(press);
        }

        // POST: Presses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var press = await _context.Press.FindAsync(id);
            _context.Press.Remove(press);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PressExists(int id)
        {
            return _context.Press.Any(e => e.Id == id);
        }
    }
}
