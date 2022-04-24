using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class BookTicketsController : Controller
    {
        private readonly ATMSDbContext _context;
        private IWebHostEnvironment _env;


        public BookTicketsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BookTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookTickets.ToListAsync());
        }

        // GET: BookTickets/Details/5
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

        // GET: BookTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BT bt)
        { 
            BookTickets bookTickets = new BookTickets();

            bookTickets.PassengerName = bt.PassengerName;
            bookTickets.PassportNumber = bt.PassportNumber;
            bookTickets.Email = bt.Email;
            bookTickets.MobileNumber = bt.MobileNumber;
            bookTickets.FlightName = bt.FlightName;
            bookTickets.Destination = bt.Destination;
            bookTickets.Date = bt.Date;
            bookTickets.Time = bt.Time;
            bookTickets.Class = bt.Class;
            bookTickets.RequiredSeat = bt.RequiredSeat;
            bookTickets.PaidPrice = bt.PaidPrice;
            bookTickets.SeatNumber = bt.SeatNumber;
            bookTickets.GateNumber = bt.GateNumber;
            bookTickets.TicketNumber = bt.TicketNumber;

            if (bt.PaidRecipt != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, bt.PaidRecipt.FileName);

                bt.PaidRecipt.CopyTo(new FileStream(filePath, FileMode.Create));
                bookTickets.PaidRecipt = bt.PaidRecipt.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(bookTickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }


        // GET: BookTickets/Edit/5
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

        // POST: BookTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BT bt)
        {
            if (id != bt.Id)
            {
                return NotFound();
            }
            BookTickets bookTickets = new BookTickets();
            bookTickets.Id = bt.Id;
            bookTickets.PassengerName = bt.PassengerName;
            bookTickets.PassportNumber = bt.PassportNumber;
            bookTickets.Email = bt.Email;
            bookTickets.MobileNumber = bt.MobileNumber;
            bookTickets.FlightName = bt.FlightName;
            bookTickets.Destination = bt.Destination;
            bookTickets.Date = bt.Date;
            bookTickets.Time = bt.Time;
            bookTickets.Class = bt.Class;
            bookTickets.RequiredSeat = bt.RequiredSeat;
            bookTickets.PaidPrice = bt.PaidPrice;
            bookTickets.SeatNumber = bt.SeatNumber;
            bookTickets.GateNumber = bt.GateNumber;
            bookTickets.TicketNumber = bt.TicketNumber;

            if (bt.PaidRecipt != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, bt.PaidRecipt.FileName);

                bt.PaidRecipt.CopyTo(new FileStream(filePath, FileMode.Create));
                bookTickets.PaidRecipt = bt.PaidRecipt.FileName;
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

        // GET: BookTickets/Delete/5
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

        // POST: BookTickets/Delete/5
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
