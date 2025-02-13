using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Dostavljac")]
    public class DostavaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DostavaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult PregledDostava()
        {
            var pendingDostave = _context.Dostave
                .Include(d => d.Narudzba)
                .Where(d => !d.Prihvacena)
                .ToList();

            var acceptedDostave = _context.Dostave
                .Include(d => d.Narudzba)
                .Where(d => d.Prihvacena)
                .ToList();

            var model = new PregledDostavaViewModel
            {
                PendingDostave = pendingDostave,
                AcceptedDostave = acceptedDostave
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PrihvatiDostavu(int id)
        {
            var dostava = _context.Dostave.Include(d => d.Narudzba).FirstOrDefault(d => d.Id == id);
            if (dostava != null)
            {
                dostava.PrihvatiDostavu();
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(PregledDostava));
        }

        [HttpPost]
        public IActionResult OdbijDostavu(int id)
        {
            var dostava = _context.Dostave.Include(d => d.Narudzba).FirstOrDefault(d => d.Id == id);
            if (dostava != null)
            {
                // Dohvati vozilo koristeći VoziloId
                var vozilo = _context.Vozila.FirstOrDefault(v => v.Id == dostava.Narudzba.VoziloId);
                if (vozilo != null)
                {
                    vozilo.Dostupno = true;
                    _context.Vozila.Update(vozilo);
                }

                // Brisanje rezervacije
                var rezervacija = dostava.Narudzba;
                if (rezervacija != null)
                {
                    _context.Rezervacije.Remove(rezervacija);
                }

                // Brisanje dostave
                _context.Dostave.Remove(dostava);

                _context.SaveChanges();
            }
            return RedirectToAction(nameof(PregledDostava));
        }
    }

    public class PregledDostavaViewModel
    {
        public List<Dostava> PendingDostave { get; set; }
        public List<Dostava> AcceptedDostave { get; set; }
    }
}
