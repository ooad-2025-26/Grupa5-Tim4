using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using QRCoder;
using SmartClinic.Data;
using SmartClinic.Models;
using SmartClinic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SmartClinic.Controllers
{
    public class TerminsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;
        public TerminsController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Termins

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Termini
                .Include(t => t.Doktor)
                .Include(t => t.Pacijent)
                .Include(t => t.UslugaKlinike);

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
                .Include(t => t.Doktor)
                .FirstOrDefaultAsync(m => m.TerminId == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termins/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["PacijentId"] = new SelectList(_userManager.Users, "Id", "Email");
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv");
            return View();
        }

        // POST: Termins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("TerminId,Datum,Vrijeme,Status,PacijentId,DoktorId,UslugaId")] Termin termin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacijentId"] = new SelectList(_userManager.Users, "Id", "Email", termin.PacijentId);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // GET: Termins/Edit/5
        [Authorize(Roles = "Admin,Doktor")]
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
            ViewData["PacijentId"] = new SelectList(
    _userManager.Users.Select(u => new
    {
        u.Id,
        PunoIme = u.Ime + " " + u.Prezime
    }),
    "Id",
    "PunoIme",
    termin.PacijentId
);

            ViewData["DoktorId"] = new SelectList(
    _userManager.Users.Where(u => u.Uloga == "Doktor")
        .Select(u => new
        {
            u.Id,
            PunoIme = u.Ime + " " + u.Prezime
        }),
    "Id",
    "PunoIme",
    termin.DoktorId
);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // POST: Termins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doktor")]
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
            ViewData["PacijentId"] = new SelectList(
    _userManager.Users.Select(u => new
    {
        u.Id,
        PunoIme = u.Ime + " " + u.Prezime
    }),
    "Id",
    "PunoIme",
    termin.PacijentId
);
            ViewData["DoktorId"] = new SelectList(
    _userManager.Users.Where(u => u.Uloga == "Doktor")
        .Select(u => new
        {
            u.Id,
            PunoIme = u.Ime + " " + u.Prezime
        }),
    "Id",
    "PunoIme",
    termin.DoktorId
);
            ViewData["UslugaId"] = new SelectList(_context.UslugeKlinike, "UslugaId", "Naziv", termin.UslugaId);
            return View(termin);
        }

        // GET: Termins/Delete/5
        [Authorize(Roles = "Admin,Doktor")]
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
        [Authorize(Roles = "Admin,Doktor")]
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

        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> Zakazi()
        {
            var model = await PopuniTerminViewModel(new TerminViewModel());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> Zakazi(TerminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await PopuniTerminViewModel(model));
            }
            var minimalniDatum = DateTime.Today;
            if (model.Datum.Date < minimalniDatum)
            {
                ModelState.AddModelError("Datum", "Termin nije moguće zakazati za datum koji je već prošao.");
                return View(await PopuniTerminViewModel(model));
            }

            var pacijent = await _userManager.GetUserAsync(User);

            bool zauzet = _context.Termini.Any(t =>
                t.DoktorId == model.DoktorId &&
                t.Datum.Date == model.Datum.Date &&
                t.Vrijeme == model.Vrijeme &&
                t.Status != StatusTermina.Otkazan);

            if (zauzet)
            {
                ModelState.AddModelError("", "Odabrani termin nije dostupan.");
                return View(await PopuniTerminViewModel(model));
            }

            var termin = new Termin
            {
                Datum = model.Datum,
                Vrijeme = model.Vrijeme,
                UslugaId = model.UslugaId,
                DoktorId = model.DoktorId,
                PacijentId = pacijent.Id,
                Status = StatusTermina.Zakazan
            };

            _context.Termini.Add(termin);
            await _context.SaveChangesAsync();

            string qrTekst = $"Termin ID: {termin.TerminId}; Pacijent ID: {pacijent.Id}; Datum: {termin.Datum:dd.MM.yyyy}; Vrijeme: {termin.Vrijeme}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(qrTekst, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);

            byte[] qrBytes = qrCode.GetGraphic(20);
            string qrBase64 = Convert.ToBase64String(qrBytes);

            var qrKod = new QRKod
            {
                VrijednostKoda = qrBase64,
                DatumGenerisanja = DateTime.Now,
                TerminId = termin.TerminId
            };

            _context.QRKodovi.Add(qrKod);
            await _context.SaveChangesAsync();

            return RedirectToAction("Potvrda", new { id = termin.TerminId });
        }
        private async Task<TerminViewModel> PopuniTerminViewModel(TerminViewModel model)
        {
            model.Usluge = _context.UslugeKlinike
                .Select(u => new SelectListItem
                {
                    Value = u.UslugaId.ToString(),
                    Text = u.Naziv
                })
                .ToList();

            if (model.UslugaId != 0)
            {
                var usluga = await _context.UslugeKlinike
                    .FirstOrDefaultAsync(u => u.UslugaId == model.UslugaId);

                var doktori = await _userManager.GetUsersInRoleAsync("Doktor");

                model.Doktori = doktori
                    .Where(d => !string.IsNullOrWhiteSpace(d.Specijalizacija)
                                && usluga != null
                                && d.Specijalizacija.Trim().ToLower() == usluga.Oblast.Trim().ToLower())
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id,
                        Text = d.Ime + " " + d.Prezime
                    })
                    .ToList();
            }
            else
            {
                model.Doktori = new List<SelectListItem>();
            }


            model.VrijemeOpcije = new List<SelectListItem>
    {
        new SelectListItem { Value = "08:00", Text = "08:00" },
        new SelectListItem { Value = "09:00", Text = "09:00" },
        new SelectListItem { Value = "10:00", Text = "10:00" },
        new SelectListItem { Value = "11:00", Text = "11:00" },
        new SelectListItem { Value = "12:00", Text = "12:00" },
        new SelectListItem { Value = "13:00", Text = "13:00" },
        new SelectListItem { Value = "14:00", Text = "14:00" },
        new SelectListItem { Value = "15:00", Text = "15:00" }
    };

            return model;
        }


        [Authorize]
        public async Task<IActionResult> Potvrda(int id)
        {
            var termin = await _context.Termini
                .Include(t => t.UslugaKlinike)
                .Include(t => t.Doktor)
                .Include(t => t.Pacijent)
                .FirstOrDefaultAsync(t => t.TerminId == id);

            if (termin == null)
            {
                return NotFound();
            }

            var qrKod = await _context.QRKodovi
                .FirstOrDefaultAsync(q => q.TerminId == id);

            ViewBag.QRKod = qrKod;

            return View(termin);
        }

        [Authorize(Roles = "Pacijent")]

        public async Task<IActionResult> GetDoktoriZaUslugu(int uslugaId)
        {
            var usluga = await _context.UslugeKlinike
                .FirstOrDefaultAsync(u => u.UslugaId == uslugaId);

            if (usluga == null)
                return Json(new List<object>());

            var doktori = await _userManager.GetUsersInRoleAsync("Doktor");

            var rezultat = doktori
                .Where(d => d.Specijalizacija != null &&
                            d.Specijalizacija.Trim().ToLower() == usluga.Oblast.Trim().ToLower())
                .Select(d => new
                {
                    id = d.Id,
                    ime = d.Ime + " " + d.Prezime
                })
                .ToList();

            return Json(rezultat);
        }

        [HttpGet]
        public IActionResult GetSlobodnaVremena(string doktorId, DateTime datum)
        {
            var svaVremena = new List<string>
    {
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00"
    };

            var zauzeta = _context.Termini
                .Where(t =>
                    t.DoktorId == doktorId &&
                    t.Datum.Date == datum.Date &&
                    t.Status != StatusTermina.Otkazan)
                .Select(t => t.Vrijeme)
                .ToList();

            var slobodna = svaVremena
                .Except(zauzeta)
                .ToList();

            return Json(slobodna);
        }
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> MojiTermini()
        {
            var korisnik = await _userManager.GetUserAsync(User);

            var termini = await _context.Termini
                .Include(t => t.UslugaKlinike)
                .Include(t => t.Doktor)
                .Where(t => t.PacijentId == korisnik.Id)
                .OrderBy(t => t.Datum)
                .ToListAsync();

            return View(termini);
        }
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> IzmijeniTermin(int id)
        {
            var termin = await _context.Termini
                .FirstOrDefaultAsync(t => t.TerminId == id);

            if (termin == null)
                return NotFound();

            ViewBag.TerminId = id;

            var model = new TerminViewModel
            {
                Datum = termin.Datum,
                Vrijeme = termin.Vrijeme,
                DoktorId = termin.DoktorId,
                UslugaId = termin.UslugaId
            };

            return View(await PopuniTerminViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> IzmijeniTermin(int id, TerminViewModel model)
        {
            var termin = await _context.Termini
                .FirstOrDefaultAsync(t => t.TerminId == id);

            if (termin == null)
                return NotFound();

            ViewBag.TerminId = id;

            bool zauzet = _context.Termini.Any(t =>
                t.TerminId != id &&
                t.DoktorId == model.DoktorId &&
                t.Datum.Date == model.Datum.Date &&
                t.Vrijeme == model.Vrijeme &&
                t.Status != StatusTermina.Otkazan);

            if (zauzet)
            {
                ModelState.AddModelError("", "Termin nije dostupan.");

                return View(await PopuniTerminViewModel(model));
            }

            termin.Datum = model.Datum;
            termin.Vrijeme = model.Vrijeme;
            termin.DoktorId = model.DoktorId;
            termin.UslugaId = model.UslugaId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MojiTermini));
        }
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> OtkaziTermin(int id)
        {
            var korisnik = await _userManager.GetUserAsync(User);

            var termin = await _context.Termini
                .Include(t => t.UslugaKlinike)
                .Include(t => t.Doktor)
                .FirstOrDefaultAsync(t => t.TerminId == id && t.PacijentId == korisnik.Id);

            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }
        [HttpPost, ActionName("OtkaziTermin")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> OtkaziTerminConfirmed(int id)
        {
            var korisnik = await _userManager.GetUserAsync(User);

            var termin = await _context.Termini
                .FirstOrDefaultAsync(t => t.TerminId == id && t.PacijentId == korisnik.Id);

            if (termin == null)
            {
                return NotFound();
            }

            termin.Status = StatusTermina.Otkazan;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MojiTermini));
        }
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> RasporedDoktora()
        {
            var doktor = await _userManager.GetUserAsync(User);

            if (doktor == null)
            {
                return Challenge();
            }

            var termini = await _context.Termini
                .Include(t => t.Pacijent)
                .Include(t => t.UslugaKlinike)
                .Where(t => t.DoktorId == doktor.Id)
                .OrderBy(t => t.Datum)
                .ThenBy(t => t.Vrijeme)
                .ToListAsync();

            return View(termini);
        }

        [Authorize(Roles = "Admin,Doktor")]
        public IActionResult SkenirajQR()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Doktor")]
        public async Task<IActionResult> ObradiQR([FromBody] string qrText)
        {
            try
            {
                var dio = qrText.Split(';')[0];

                int terminId = int.Parse(
                    dio.Replace("Termin ID:", "").Trim()
                );

                var termin = await _context.Termini
                    .FirstOrDefaultAsync(t => t.TerminId == terminId);

                if (termin == null)
                    return BadRequest("Termin nije pronađen.");

                termin.Status = StatusTermina.Realizovan;

                await _context.SaveChangesAsync();

                return Ok("Termin uspješno realizovan.");
            }
            catch
            {
                return BadRequest("Neispravan QR kod.");
            }
        }

        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> PrikaziQR(int id)
        {
            var termin = await _context.Termini
    .Include(t => t.QRKod)
    .Include(t => t.UslugaKlinike)
    .Include(t => t.Doktor)
    .Include(t => t.Pacijent)

                .FirstOrDefaultAsync(t => t.TerminId == id);

            if (termin == null)
                return NotFound();

            return View(termin);
        }
    }
}
    
