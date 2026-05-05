using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;

namespace SmartClinic.Controllers
{
    public class RasporedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RasporedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rasporeds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rasporedi.ToListAsync());
        }

        // GET: Rasporeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi
                .FirstOrDefaultAsync(m => m.RasporedId == id);
            if (raspored == null)
            {
                return NotFound();
            }

            return View(raspored);
        }

        // GET: Rasporeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rasporeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RasporedId,Datum,PocetakSmjene,KrajSmjene,DoktorId")] Raspored raspored)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raspored);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raspored);
        }

        // GET: Rasporeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi.FindAsync(id);
            if (raspored == null)
            {
                return NotFound();
            }
            return View(raspored);
        }

        // POST: Rasporeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RasporedId,Datum,PocetakSmjene,KrajSmjene,DoktorId")] Raspored raspored)
        {
            if (id != raspored.RasporedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raspored);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RasporedExists(raspored.RasporedId))
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
            return View(raspored);
        }

        // GET: Rasporeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raspored = await _context.Rasporedi
                .FirstOrDefaultAsync(m => m.RasporedId == id);
            if (raspored == null)
            {
                return NotFound();
            }

            return View(raspored);
        }

        // POST: Rasporeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raspored = await _context.Rasporedi.FindAsync(id);
            if (raspored != null)
            {
                _context.Rasporedi.Remove(raspored);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RasporedExists(int id)
        {
            return _context.Rasporedi.Any(e => e.RasporedId == id);
        }
    }
}
