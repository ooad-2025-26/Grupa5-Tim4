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
    public class TerminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TerminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Termins
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Termini.Include(t => t.Pacijent).Include(t => t.UslugaKlinike);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Termins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termini
                .Include(t => t.Pacijent)
                .Include(t => t.UslugaKlinike)
                .FirstOrDefaultAsync(m => m.TerminId == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termins/Create
        public IActionResult Create()
        {
            ViewData["PacijentId"] = new SelectList(_context.Korisnici, "Id", "Email");
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv");
            return View();
        }

        // POST: Termins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerminId,Datum,Vrijeme,Status,PacijentId,DoktorId,UslugaId")] Termin termin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacijentId"] = new SelectList(_context.Korisnici, "Id", "Email", termin.PacijentId);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // GET: Termins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termini.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }
            ViewData["PacijentId"] = new SelectList(_context.Korisnici, "Id", "Email", termin.PacijentId);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // POST: Termins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TerminId,Datum,Vrijeme,Status,PacijentId,DoktorId,UslugaId")] Termin termin)
        {
            if (id != termin.TerminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.TerminId))
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
            ViewData["PacijentId"] = new SelectList(_context.Korisnici, "Id", "Email", termin.PacijentId);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // GET: Termins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termini
                .Include(t => t.Pacijent)
                .Include(t => t.UslugaKlinike)
                .FirstOrDefaultAsync(m => m.TerminId == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // POST: Termins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termin = await _context.Termini.FindAsync(id);
            if (termin != null)
            {
                _context.Termini.Remove(termin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminExists(int id)
        {
            return _context.Termini.Any(e => e.TerminId == id);
        }
    }
}
