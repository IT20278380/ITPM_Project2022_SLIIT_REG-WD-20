using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class CBookTicketsController : Controller
    {
        private readonly ATMSDbContext _context;

        public CBookTicketsController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: CBookTickets
        public async Task<IActionResult> Index(string book)
        {
            ViewData["GetBookTickets"] = book;

            var bookQuery = from tickets in _context.BookTickets select tickets;
            if (!string.IsNullOrEmpty(book))
            {
                bookQuery = bookQuery.Where(tickets => tickets.PassengerName.Contains(book));
            }
            return View(await bookQuery.AsNoTracking().ToListAsync());
        }

        // GET: CBookTickets/Details/5
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

        
        // GET: CBookTickets/Edit/5
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

        // POST: CBookTickets/Edit/5
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

        // GET: CBookTickets/Edit/5
        public async Task<IActionResult> update(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(int id, [Bind("Id,PassengerName,PassportNumber,Email,MobileNumber,FlightName,Destination,Date,Time,Class,RequiredSeat,PaidPrice,SeatNumber,GateNumber,TicketNumber,PaidRecipt")] BookTickets bookTickets)
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
        // GET: CBookTickets
        public IActionResult Notification()
        {
            return RedirectToAction("Nview", "CNotifications");
        }

        public int Count()
        {
            int data; ;
            
            var dbresult = _context.Notification.CountAsync().Result;
            data = dbresult;
            return data;
        }

        [HttpPost]
        [Obsolete]
        public FileResult Export(string BookTickInput)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {

                StringReader sr = new StringReader(BookTickInput);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 70f, 0f, 70f, 0f);

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "BookTickets.pdf");
            }
        }

        private bool BookTicketsExists(int id)
        {
            return _context.BookTickets.Any(e => e.Id == id);
        }
    }
}
