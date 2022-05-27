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
using System.Text;
using HtmlAgilityPack;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class OrderListsController : Controller
    {
        private readonly ATMSDbContext _context;

        public OrderListsController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: OrderLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderList.ToListAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            var orderList = await _context.OrderList.FindAsync(id);
            _context.OrderList.Remove(orderList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Obsolete]
        public FileResult Export(string OrderInput)
        {
            HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
            HtmlNode.ElementsFlags["input"] = HtmlElementFlag.Closed;
            HtmlDocument doc = new HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(OrderInput);
            OrderInput = doc.DocumentNode.OuterHtml;

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                Encoding unicode = Encoding.UTF8;
                StringReader sr = new StringReader(OrderInput);
                Document pdfDoc = new Document(PageSize.A4, 60f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "OrderLists.pdf");
            }
        }
        private bool OrderListExists(int id)
        {
            return _context.OrderList.Any(e => e.Id == id);
        }
    }
}
