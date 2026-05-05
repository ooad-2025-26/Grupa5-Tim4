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
    public class UslugaKlinikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UslugaKlinikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UslugaKlinikes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UslugeKlinike.ToListAsync());
        }

        // GET: UslugaKlinikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaKlinike = await _context.UslugeKlinike
                .FirstOrDefaultAsync(m => m.UslugaId == id);
            if (uslugaKlinike == null)
            {
                return NotFound();
            }

            return View(uslugaKlinike);
        }

        // GET: UslugaKlinikes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UslugaKlinikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UslugaId,Naziv,Opis,TrajanjeUsluge,Cijena")] UslugaKlinike uslugaKlinike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uslugaKlinike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uslugaKlinike);
        }

        // GET: UslugaKlinikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaKlinike = await _context.UslugeKlinike.FindAsync(id);
            if (uslugaKlinike == null)
            {
                return NotFound();
            }
            return View(uslugaKlinike);
        }

        // POST: UslugaKlinikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UslugaId,Naziv,Opis,TrajanjeUsluge,Cijena")] UslugaKlinike uslugaKlinike)
        {
            if (id != uslugaKlinike.UslugaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uslugaKlinike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaKlinikeExists(uslugaKlinike.UslugaId))
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
            return View(uslugaKlinike);
        }

        // GET: UslugaKlinikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaKlinike = await _context.UslugeKlinike
                .FirstOrDefaultAsync(m => m.UslugaId == id);
            if (uslugaKlinike == null)
            {
                return NotFound();
            }

            return View(uslugaKlinike);
        }

        // POST: UslugaKlinikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uslugaKlinike = await _context.UslugeKlinike.FindAsync(id);
            if (uslugaKlinike != null)
            {
                _context.UslugeKlinike.Remove(uslugaKlinike);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaKlinikeExists(int id)
        {
            return _context.UslugeKlinike.Any(e => e.UslugaId == id);
        }
    }
}
