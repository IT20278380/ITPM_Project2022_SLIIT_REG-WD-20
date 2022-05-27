using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using HtmlAgilityPack;

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


        [HttpPost]
        [Obsolete]
        public FileResult Export(string FlightInput)
        {
            HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
            HtmlNode.ElementsFlags["input"] = HtmlElementFlag.Closed;
            HtmlNode.ElementsFlags["form"] = HtmlElementFlag.Closed;
            HtmlDocument doc = new HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(FlightInput);
            FlightInput = doc.DocumentNode.OuterHtml;

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                Encoding unicode = Encoding.UTF8;
                StringReader sr = new StringReader(FlightInput);
                Document pdfDoc = new Document(PageSize.A4, 30f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "FlightLists.pdf");
            }
        }
    }
}
