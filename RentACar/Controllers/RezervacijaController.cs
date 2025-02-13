using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;
using RentACar.ViewModels;
using System;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    [Authorize]
    public class RezervacijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;

        public RezervacijaController(ApplicationDbContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRezervacija(RezervacijaViewModel model)
        {
            if (model == null)
            {
                TempData["ErrorMessage"] = "Invalid data.";
                return RedirectToAction("Details", "Vozilo", new { id = model.VoziloId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var vozilo = await _context.Vozila.FindAsync(model.VoziloId);
            if (vozilo == null)
            {
                TempData["ErrorMessage"] = "Invalid vehicle.";
                return RedirectToAction("Details", "Vozilo", new { id = model.VoziloId });
            }

            if (!Enum.TryParse(model.VrstaPlacanja, true, out VrstaPlacanja vrstaPlacanja))
            {
                TempData["ErrorMessage"] = "Invalid payment method.";
                return RedirectToAction("Details", "Vozilo", new { id = model.VoziloId });
            }

            // Provjeravamo dostupnost vozila za odabrane datume
            var rezervacijeZaVozilo = await _context.Rezervacije
                .Where(r => r.VoziloId == model.VoziloId &&
                            r.DatumPovratka > model.DatumPreuzimanja &&
                            r.DatumPreuzimanja < model.DatumPovratka)
                .ToListAsync();

            if (rezervacijeZaVozilo.Any())
            {
                TempData["ErrorMessage"] = "Vehicle is not available for the selected dates.";
                return RedirectToAction("Details", "Vozilo", new { id = model.VoziloId });
            }

            // Kreiranje nove rezervacije
            var rezervacija = new Rezervacija
            {
                DatumRezervacije = DateTime.Now,
                DatumPreuzimanja = model.DatumPreuzimanja,
                DatumPovratka = model.DatumPovratka,
                Iznos = model.Iznos,
                VoziloId = model.VoziloId,
                Narucilac = user,
                VrstaPlacanja = vrstaPlacanja
            };

            vozilo.Dostupno = false;
            _context.Rezervacije.Add(rezervacija);
            _context.Vozila.Update(vozilo);

            await _context.SaveChangesAsync();

            if (model.Dostava != null && !string.IsNullOrEmpty(model.Dostava.Adresa))
            {
                var dostava = new Dostava
                {
                    NarudzbaId = rezervacija.Id,
                    Adresa = model.Dostava.Adresa,
                    Prihvacena = false
                };
                _context.Dostave.Add(dostava);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reservation created successfully.";
            return RedirectToAction("Details", "Vozilo", new { id = model.VoziloId });
        }


        public async Task<IActionResult> MojeRezervacije()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var rezervacije = await _context.Rezervacije
                .Where(r => r.Narucilac.Id == user.Id)
                .Select(r => new MojeRezervacijeViewModel
                {
                    Rezervacija = r,
                    Vozilo = _context.Vozila.FirstOrDefault(v => v.Id == r.VoziloId),
                    Dostava = _context.Dostave.FirstOrDefault(d => d.NarudzbaId == r.Id)
                })
                .ToListAsync();

            return View(rezervacije);
        }


        [HttpPost]
        public async Task<IActionResult> OtkaziRezervaciju(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var rezervacija = await _context.Rezervacije
      .FirstOrDefaultAsync(r => r.Id == id && r.Narucilac.Id == user.Id);
            var dostava = _context.Dostave.Include(d => d.Narudzba).FirstOrDefault(d => d.Narudzba.Id == id); // Ovdje promijenite uslov

            if (rezervacija == null)
            {
                TempData["ErrorMessageForMojeRezervacije"] = "Rezervacija nije pronađena.";
                return RedirectToAction("MojeRezervacije");
            }

            /*if (dostava == null)
            {
                TempData["ErrorMessage"] = "Dostava nije pronađena.";
                return RedirectToAction("MojeRezervacije");
            }*/

            var currentTime = DateTime.Now;
            var reservationTime = rezervacija.DatumRezervacije;

            if ((currentTime - reservationTime).TotalHours > 72)
            {
                TempData["ErrorMessageForMojeRezervacije"] = "Rezervacija se može otkazati samo u roku od 72 sata od trenutka pravljenja.";
                return RedirectToAction("MojeRezervacije");
            }

            var vozilo = await _context.Vozila.FirstOrDefaultAsync(v => v.Id == rezervacija.VoziloId);
            if (vozilo != null)
            {
                vozilo.Dostupno = true;
                _context.Vozila.Update(vozilo);
            }

            _context.Rezervacije.Remove(rezervacija);
            if (dostava != null)
            {
                _context.Dostave.Remove(dostava);
            }
            await _context.SaveChangesAsync();

            TempData["SuccessMessageForMojeRezervacije"] = "Rezervacija je uspješno otkazana.";
            return RedirectToAction("MojeRezervacije");

        }

    }
}

