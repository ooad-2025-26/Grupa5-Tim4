using Microsoft.AspNetCore.Mvc;
using SmartClinic.Models;
using System.Diagnostics;

namespace SmartClinic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Usluge()
        {
            return View();
        }

        public IActionResult DetaljiUsluge(string naziv)
        {
            ViewBag.Naziv = naziv;
            return View();
        }
        public IActionResult ONama()
        {
            return View();
        }
        public IActionResult Kontakt()
        {
            return View();
        }
    }
}
