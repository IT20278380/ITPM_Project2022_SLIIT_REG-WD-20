using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class FirstClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private static string EnterPassportNumber;
        private IWebHostEnvironment _env;
        public FirstClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: FirstClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.FirstClassFood.ToListAsync());
        }

        public async Task<IActionResult> Status(string PassportNumber)
        {
            EnterPassportNumber = PassportNumber;
            var avaibleFood = await _context.FirstClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
        }

        // GET: FirstClassFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstClassFood = await _context.FirstClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firstClassFood == null)
            {
                return NotFound();
            }

            return View(firstClassFood);
        }

        // GET: FirstClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FirstClassFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Image,Price,FoodStatus")] FCF fcf)
        {
            FirstClassFood firstClassFood = new FirstClassFood();

            firstClassFood.FoodName = fcf.FoodName;
            firstClassFood.Price = fcf.Price;
            firstClassFood.FoodStatus = fcf.FoodStatus;

            if (fcf.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, fcf.Image.FileName);

                fcf.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                firstClassFood.Image = fcf.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(firstClassFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firstClassFood);
        }

        // GET: FirstClassFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstClassFood = await _context.FirstClassFood.FindAsync(id);
            if (firstClassFood == null)
            {
                return NotFound();
            }
            return View(firstClassFood);
        }

        // POST: FirstClassFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Image,Price,FoodStatus")] FirstClassFood firstClassFood)
        {
            if (id != firstClassFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firstClassFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirstClassFoodExists(firstClassFood.Id))
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
            return View(firstClassFood);
        }

        // GET: FirstClassFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstClassFood = await _context.FirstClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firstClassFood == null)
            {
                return NotFound();
            }

            return View(firstClassFood);
        }

        // POST: FirstClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firstClassFood = await _context.FirstClassFood.FindAsync(id);
            _context.FirstClassFood.Remove(firstClassFood);
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
        private bool FirstClassFoodExists(int id)
        {
            return _context.FirstClassFood.Any(e => e.Id == id);
        }
    }
}
