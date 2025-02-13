using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Authorize]
    public class VoziloController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VoziloController> _logger;


        public VoziloController(ApplicationDbContext context, ILogger<VoziloController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Details(int id)
        {
            var vozilo = _context.Vozila.Include(v => v.MaticnaPoslovnicaId).FirstOrDefault(v => v.Id == id);

            if (vozilo == null)
            {
                return NotFound(); // Ako vozilo nije pronađeno, vraćamo Not Found
            }

            // Dohvaćanje povezane poslovnice iz vozila
            var poslovnica = vozilo.MaticnaPoslovnicaId;

            // Provjera je li poslovnica pronađena
            /*if (poslovnica == null)
            {
                return NotFound(); // Ako poslovnica nije pronađena, vraćamo Not Found
            }*/

           

            var rezervacije = _context.Rezervacije.Where(r => r.VoziloId == id).ToList();

            var model = new DetaljiVozilaViewModel
            {
                Vozilo = vozilo,
                Poslovnica = poslovnica,

                Rezervacije = rezervacije
            };

            return View(model);
        }

        // GET: Vozilo
        public IActionResult Index()
        {
            var vozila = _context.Vozila.ToList();
            return View(vozila);
        }

        // GET: Vozilo/Create
        // GET: Vozilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozilo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Proizvodjac,Model,Cijena,Slika,Opis,Tip,RegistarskeTablice,Navigacija,Transmisija,Gorivo,MaticnaPoslovnicaId,Dostupno,PutnickoVozilo_Tip,BrojSjedista,Tempomat,TransportnoVozilo_Tip,Nosivost,Duzina,Prikolica")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozilo);
                await _context.SaveChangesAsync();
                return RedirectToAction("ExploreCars", "Home"); // Ovo preusmjerava na akciju ExploreCars u Home kontroleru
            }
            return View(vozilo);
        }

    }
}
