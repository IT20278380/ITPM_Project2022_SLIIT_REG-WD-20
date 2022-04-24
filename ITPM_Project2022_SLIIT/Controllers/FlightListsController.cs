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

        // GET: FlightLists
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.FlightList.ToListAsync());
        }*/

        //Search
        public async Task<IActionResult> Index(string flightSearch)
        {
            ViewData["GetFlightDetails"] = flightSearch;

            var flightQuery = from flight in _context.FlightList select flight;
            if (!string.IsNullOrEmpty(flightSearch))
            {
                flightQuery = flightQuery.Where(flight => flight.Destination.Contains(flightSearch));
            }
            return View(await flightQuery.AsNoTracking().ToListAsync());
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

        // GET: FlightLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightList = await _context.FlightList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flightList == null)
            {
                return NotFound();
            }

            return View(flightList);
        }

        // GET: FlightLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FlightName,Date,Time,Destination,FirstClassPrice,BsClassPrice,PriEconomyClassPrice,EconomyClassPrice")] FlightList flightList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightList);
        }

        // GET: FlightLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightList = await _context.FlightList.FindAsync(id);
            if (flightList == null)
            {
                return NotFound();
            }
            return View(flightList);
        }

        // POST: FlightLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightName,Date,Time,Destination,FirstClassPrice,BsClassPrice,PriEconomyClassPrice,EconomyClassPrice")] FlightList flightList)
        {
            if (id != flightList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightListExists(flightList.Id))
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
            return View(flightList);
        }

        // GET: FlightLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightList = await _context.FlightList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flightList == null)
            {
                return NotFound();
            }

            return View(flightList);
        }

        // POST: FlightLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightList = await _context.FlightList.FindAsync(id);
            _context.FlightList.Remove(flightList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightListExists(int id)
        {
            return _context.FlightList.Any(e => e.Id == id);
        }
        /*-----------------------------------------------------------------------------------*/
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
