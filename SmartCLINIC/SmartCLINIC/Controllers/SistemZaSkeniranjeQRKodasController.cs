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
    public class SistemZaSkeniranjeQRKodasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SistemZaSkeniranjeQRKodasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SistemZaSkeniranjeQRKodas
        public async Task<IActionResult> Index()
        {
            return View(await _context.SistemiZaSkeniranje.ToListAsync());
        }

        // GET: SistemZaSkeniranjeQRKodas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemZaSkeniranjeQRKoda = await _context.SistemiZaSkeniranje
                .FirstOrDefaultAsync(m => m.UredjajId == id);
            if (sistemZaSkeniranjeQRKoda == null)
            {
                return NotFound();
            }

            return View(sistemZaSkeniranjeQRKoda);
        }

        // GET: SistemZaSkeniranjeQRKodas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SistemZaSkeniranjeQRKodas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UredjajId,StatusUredjaja,LokacijaUredjaja")] SistemZaSkeniranjeQRKoda sistemZaSkeniranjeQRKoda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sistemZaSkeniranjeQRKoda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sistemZaSkeniranjeQRKoda);
        }

        // GET: SistemZaSkeniranjeQRKodas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemZaSkeniranjeQRKoda = await _context.SistemiZaSkeniranje.FindAsync(id);
            if (sistemZaSkeniranjeQRKoda == null)
            {
                return NotFound();
            }
            return View(sistemZaSkeniranjeQRKoda);
        }

        // POST: SistemZaSkeniranjeQRKodas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UredjajId,StatusUredjaja,LokacijaUredjaja")] SistemZaSkeniranjeQRKoda sistemZaSkeniranjeQRKoda)
        {
            if (id != sistemZaSkeniranjeQRKoda.UredjajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistemZaSkeniranjeQRKoda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemZaSkeniranjeQRKodaExists(sistemZaSkeniranjeQRKoda.UredjajId))
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
            return View(sistemZaSkeniranjeQRKoda);
        }

        // GET: SistemZaSkeniranjeQRKodas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemZaSkeniranjeQRKoda = await _context.SistemiZaSkeniranje
                .FirstOrDefaultAsync(m => m.UredjajId == id);
            if (sistemZaSkeniranjeQRKoda == null)
            {
                return NotFound();
            }

            return View(sistemZaSkeniranjeQRKoda);
        }

        // POST: SistemZaSkeniranjeQRKodas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sistemZaSkeniranjeQRKoda = await _context.SistemiZaSkeniranje.FindAsync(id);
            if (sistemZaSkeniranjeQRKoda != null)
            {
                _context.SistemiZaSkeniranje.Remove(sistemZaSkeniranjeQRKoda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemZaSkeniranjeQRKodaExists(int id)
        {
            return _context.SistemiZaSkeniranje.Any(e => e.UredjajId == id);
        }
    }
}
