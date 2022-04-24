using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class BookController : Controller
    {
        private readonly ATMSDbContext _context;

        public BookController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookTickets.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTickets = await _context.BookTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookTickets == null)
            {
                return NotFound();
            }

            return View(bookTickets);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BookTickets bookTickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookTickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookTickets);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTickets = await _context.BookTickets.FindAsync(id);
            if (bookTickets == null)
            {
                return NotFound();
            }
            return View(bookTickets);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BookTickets bookTickets)
        {
            if (id != bookTickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookTickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTicketsExists(bookTickets.Id))
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
            return View(bookTickets);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTickets = await _context.BookTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookTickets == null)
            {
                return NotFound();
            }

            return View(bookTickets);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookTickets = await _context.BookTickets.FindAsync(id);
            _context.BookTickets.Remove(bookTickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookTicketsExists(int id)
        {
            return _context.BookTickets.Any(e => e.Id == id);
        }
    }
}
