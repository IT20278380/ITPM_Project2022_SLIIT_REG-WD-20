using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ITPM_Project2022_SLIIT.Models
{
    public class HFlightListsController : Controller
    {
        private readonly ATMSDbContext _context;

        public HFlightListsController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: HFlightLists
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

        // GET: HFlightLists/Details/5
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

        // GET: HFlightLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HFlightLists/Create
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

        // GET: HFlightLists/Edit/5
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

        // POST: HFlightLists/Edit/5
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

                    Notification notification = new Notification();
                    notification.FlightName = flightList.FlightName;
                    notification.Destination = flightList.Destination;
                    notification.Date = flightList.Date;
                    notification.Time = flightList.Time;

                    _context.Add(notification);
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
                public async Task<IActionResult> UpdateFlightDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightDetails = await _context.FlightList.FindAsync(id);
            if (flightDetails == null)
            {
                return NotFound();
            }
            return View(flightDetails);
        }
        public async Task<IActionResult> UpdateFlightDetails(int id, [Bind("id,FlightName,Date,Time,Destination")] FlightList flightDetails)
        {
            if (id != flightDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightListExists(flightDetails.Id))
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
            return View(flightDetails);

        }

        public async Task<IActionResult> UpdateTicketPrice(int id, [Bind("FirstClassPrice,BsClassPrice,PriEconomyClassPrice,EconomyClassPrice")] FlightList flightList)
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

        // GET: HFlightLists/Delete/5
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

        // POST: HFlightLists/Delete/5
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

        public IActionResult ViewA()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public FileResult Export(string HflightInput)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {

                StringReader sr = new StringReader(HflightInput);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 70f, 0f, 70f, 0f);
              
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "flightList.pdf");
            }
        }
    }

}
