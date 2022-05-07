using ITPM_Project2022_SLIIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ATMSDbContext _context;

        public HomeController(ATMSDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult FindView()
        {
            return View();
        }
        [Authorize]
        public IActionResult BookNow()
        {
            return RedirectToAction("PassengerFlightList", "FlightLists");
        }

        //find order class
        public async Task<IActionResult> Find(string PassportNumber)
        {
            if (PassportNumber == null)
            {
                TempData["message"] = "Enter Passport Number !";
                return RedirectToAction("Index", "Home");
            }

            var bookTickets = await _context.BookTickets
                .FirstOrDefaultAsync(m => m.PassportNumber == PassportNumber);

            System.Diagnostics.Debug.WriteLine(bookTickets);
            if (bookTickets == null)
            {
                TempData["message"] = "Wrong Passport Number !";
                return RedirectToAction("Index", "Home");
            }
            if(bookTickets.Class == "First Class")
            {
                return RedirectToAction("Status", "FirstClassFoods",new { PassportNumber = PassportNumber });

            }else if(bookTickets.Class == "Business Class")
            {
                return RedirectToAction("Status", "BsClassFoods", new { PassportNumber = PassportNumber });

            }else if (bookTickets.Class == "Economy Class")
            {
                return RedirectToAction("Status", "EconomyClassFoods", new { PassportNumber = PassportNumber });

            }else if (bookTickets.Class == "Primium Economy Class")
            {
                return RedirectToAction("Status", "PriEconomyClassFoods", new { PassportNumber = PassportNumber });
            }else
            {
                return RedirectToAction("Index", "Home");
            }

            //return RedirectToAction("AboutUs", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
