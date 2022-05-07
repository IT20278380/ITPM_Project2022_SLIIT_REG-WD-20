using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class FlightListsController : Controller
    {
        private readonly ATMSDbContext _context;

        public FlightListsController(ATMSDbContext context)
        {
            _context = context;
        }

        //Pasenger View
        public async Task<IActionResult> PassengerFlightList(string flightSearch)
        {
            ViewData["GetFlightDetails"] = flightSearch;

            var flightQuery = from flight in _context.FlightList select flight;
            if (!string.IsNullOrEmpty(flightSearch))
            {
                flightQuery = flightQuery.Where(flight => flight.Destination.Contains(flightSearch));
            }
            return View(await flightQuery.AsNoTracking().ToListAsync());
        }

         public async Task<IActionResult> CreateBL([Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BookTickets bookTickets)
        {

            if (ModelState.IsValid)
            {
                _context.Add(bookTickets);
                await _context.SaveChangesAsync();

                var bt = _context.BookTickets.Max(B => B.Id);
                return RedirectToAction("Edit", "BookTickets", new {id = bt});
            }
            return View(bookTickets);
        }
    }
}
