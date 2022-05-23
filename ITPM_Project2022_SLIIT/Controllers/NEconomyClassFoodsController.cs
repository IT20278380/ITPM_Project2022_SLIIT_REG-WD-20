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
    public class NEconomyClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private IWebHostEnvironment _env;
        public NEconomyClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: NEconomyClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.EconomyClassFood.ToListAsync());
        }

        public async Task<IActionResult> order()
        {
            return RedirectToAction("Index", "OrderLists");
        }

        // GET: NEconomyClassFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var economyClassFood = await _context.EconomyClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (economyClassFood == null)
            {
                return NotFound();
            }

            return View(economyClassFood);
        }

        // GET: NEconomyClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NEconomyClassFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Image,Price,FoodStatus")] ECF ecf)
        {
            EconomyClassFood economyClassFood = new EconomyClassFood();



            economyClassFood.FoodName = ecf.FoodName;
            economyClassFood.Price = ecf.Price;
            economyClassFood.FoodStatus = ecf.FoodStatus;



            if (ecf.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, ecf.Image.FileName);



                ecf.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                economyClassFood.Image = ecf.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(economyClassFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(economyClassFood);
        }


        // GET: NEconomyClassFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var economyClassFood = await _context.EconomyClassFood.FindAsync(id);
            if (economyClassFood == null)
            {
                return NotFound();
            }
            return View(economyClassFood);
        }

        // POST: NEconomyClassFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Image,Price,FoodStatus")] EconomyClassFood economyClassFood)
        {
            if (id != economyClassFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(economyClassFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EconomyClassFoodExists(economyClassFood.Id))
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
            return View(economyClassFood);
        }

        // GET: NEconomyClassFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var economyClassFood = await _context.EconomyClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (economyClassFood == null)
            {
                return NotFound();
            }

            return View(economyClassFood);
        }

        // POST: NEconomyClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var economyClassFood = await _context.EconomyClassFood.FindAsync(id);
            _context.EconomyClassFood.Remove(economyClassFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EconomyClassFoodExists(int id)
        {
            return _context.EconomyClassFood.Any(e => e.Id == id);
        }
    }
}
