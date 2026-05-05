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
    public class QRKodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QRKodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QRKods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QRKodovi.Include(q => q.Termin);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QRKods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qRKod = await _context.QRKodovi
                .Include(q => q.Termin)
                .FirstOrDefaultAsync(m => m.QRKodId == id);
            if (qRKod == null)
            {
                return NotFound();
            }

            return View(qRKod);
        }

        // GET: QRKods/Create
        public IActionResult Create()
        {
            ViewData["TerminId"] = new SelectList(_context.Termini, "TerminId", "Vrijeme");
            return View();
        }

        // POST: QRKods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QRKodId,VrijednostKoda,DatumGenerisanja,TerminId")] QRKod qRKod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qRKod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TerminId"] = new SelectList(_context.Termini, "TerminId", "Vrijeme", qRKod.TerminId);
            return View(qRKod);
        }

        // GET: QRKods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qRKod = await _context.QRKodovi.FindAsync(id);
            if (qRKod == null)
            {
                return NotFound();
            }
            ViewData["TerminId"] = new SelectList(_context.Termini, "TerminId", "Vrijeme", qRKod.TerminId);
            return View(qRKod);
        }

        // POST: QRKods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QRKodId,VrijednostKoda,DatumGenerisanja,TerminId")] QRKod qRKod)
        {
            if (id != qRKod.QRKodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qRKod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QRKodExists(qRKod.QRKodId))
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
            ViewData["TerminId"] = new SelectList(_context.Termini, "TerminId", "Vrijeme", qRKod.TerminId);
            return View(qRKod);
        }

        // GET: QRKods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qRKod = await _context.QRKodovi
                .Include(q => q.Termin)
                .FirstOrDefaultAsync(m => m.QRKodId == id);
            if (qRKod == null)
            {
                return NotFound();
            }

            return View(qRKod);
        }

        // POST: QRKods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qRKod = await _context.QRKodovi.FindAsync(id);
            if (qRKod != null)
            {
                _context.QRKodovi.Remove(qRKod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QRKodExists(int id)
        {
            return _context.QRKodovi.Any(e => e.QRKodId == id);
        }
    }
}
