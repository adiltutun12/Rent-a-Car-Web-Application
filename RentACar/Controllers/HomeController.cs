using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RentACar.Data;
using RentACar.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<Account> userManager, ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserName = user.UserName;
                ViewBag.Roles = string.Join(", ", roles);
            }
            else
            {
                ViewBag.UserName = "Not logged in";
                ViewBag.Roles = "None";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult ExploreCars(VoziloPretragaViewModel searchModel)
        {
            var allCars = _context.Vozila.ToList();

            // Filtriranje po imenu proizvođača
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                allCars = allCars.Where(c => c.Proizvodjac.Contains(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filtriranje po minimalnoj cijeni
            if (searchModel.MinCijena.HasValue)
            {
                allCars = allCars.Where(c => c.Cijena >= searchModel.MinCijena).ToList();
            }

            // Filtriranje po maksimalnoj cijeni
            if (searchModel.MaxCijena.HasValue)
            {
                allCars = allCars.Where(c => c.Cijena < searchModel.MaxCijena).ToList();
            }

            if(searchModel.Tip == TipVozila.PUTNICKO)
            {
                allCars = allCars.Where(c => c.Tip == TipVozila.PUTNICKO).ToList();
            }

            if (searchModel.Tip == TipVozila.TRANSPORTNO)
            {
                allCars = allCars.Where(c => c.Tip == TipVozila.TRANSPORTNO).ToList();
            }

            if (!string.IsNullOrEmpty(searchModel.SortBy))
            {
                switch (searchModel.SortBy)
                {
                    case "PriceLowToHigh":
                        allCars = allCars.OrderBy(c => c.Cijena).ToList();
                        break;
                    case "PriceHighToLow":
                        allCars = allCars.OrderByDescending(c => c.Cijena).ToList();
                        break;
                    default:
                        // Ako nije odabrana opcija za sortiranje, ostavi listu nesortiranom
                        break;
                }
            }
            // Ako nisu postavljene specifične vrednosti za filtere, prikaži sva vozila
            searchModel.Cars = allCars;
            return View(searchModel);
        }


        public IActionResult CarDetails(int id)
        {
            var car = _context.Vozila.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult DeleteCar(int id)
        {
            var car = _context.Vozila.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Vozila.Remove(car);
            _context.SaveChanges();
            return RedirectToAction("ExploreCars", "Home");
        }

        public IActionResult EditCar(int id)
        {
            var car = _context.Vozila.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        public IActionResult EditCar(Vozilo car)
        {
            if (ModelState.IsValid)
            {
                _context.Vozila.Update(car);
                _context.SaveChanges();
                return RedirectToAction("ExploreCars", "Home");
            }
            return View(car);
        }

    }
}
