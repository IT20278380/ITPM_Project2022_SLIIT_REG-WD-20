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

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class BsClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private static string EnterPassportNumber;
        private IWebHostEnvironment _env;

        public BsClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BsClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.BsClassFood.ToListAsync());
        }

        public async Task<IActionResult> Status(string PassportNumber)
        {
            //var FoodStatus = 1;
            EnterPassportNumber = PassportNumber;
            var avaibleFood = await _context.BsClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
        }

        // GET: BsClassFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bsClassFood = await _context.BsClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bsClassFood == null)
            {
                return NotFound();
            }

            return View(bsClassFood);
        }

        // GET: BsClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BsClassFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Image,Price,FoodStatus")] BCF bcf)
        {
            BsClassFood bsClassFood = new BsClassFood();

            bsClassFood.FoodName = bcf.FoodName;
            bsClassFood.Price = bcf.Price;
            bsClassFood.FoodStatus = bcf.FoodStatus;

            if (bcf.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, bcf.Image.FileName);

                bcf.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                bsClassFood.Image = bcf.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(bsClassFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bsClassFood);
        }

        // GET: BsClassFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bsClassFood = await _context.BsClassFood.FindAsync(id);
            if (bsClassFood == null)
            {
                return NotFound();
            }
            return View(bsClassFood);
        }

        // POST: BsClassFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Image,Price,FoodStatus")] BsClassFood bsClassFood)
        {
            if (id != bsClassFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bsClassFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BsClassFoodExists(bsClassFood.Id))
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
            return View(bsClassFood);
        }

        // GET: BsClassFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bsClassFood = await _context.BsClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bsClassFood == null)
            {
                return NotFound();
            }

            return View(bsClassFood);
        }

        // POST: BsClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bsClassFood = await _context.BsClassFood.FindAsync(id);
            _context.BsClassFood.Remove(bsClassFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateBL([Bind("Id,FoodName,Price,Image,FlightName,Date,Time,SeatNumber")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                //var bookTickets = await _context.BookTickets.Find(EnterPassportNumber);

                var bookTickets = _context.BookTickets.Where(b => b.PassportNumber == EnterPassportNumber);


                if (bookTickets == null)
                {
                    return NotFound();
                }

                orderList.FlightName = bookTickets.Select(b => b.FlightName).First();
                orderList.Date = bookTickets.Select(b => b.Date).First();
                orderList.Time = bookTickets.Select(b => b.Time).First();
                orderList.SeatNumber = bookTickets.Select(b => b.SeatNumber).First();

                _context.Add(orderList);
                await _context.SaveChangesAsync();

                //var bt = _context.BookTickets.Max(B => B.Id);
                //return RedirectToAction("Edit", "BookTickets", new { id = bt });
                return RedirectToAction("Index", "OrderLists");
            }
            return View(orderList);
        }

        private bool BsClassFoodExists(int id)
        {
            return _context.BsClassFood.Any(e => e.Id == id);
        }
    }
}
