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
    public class NPriEconomyClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private IWebHostEnvironment _env;
        public NPriEconomyClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: NPriEconomyClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriEconomyClassFood.ToListAsync());
        }

        public async Task<IActionResult> order()
        {
            return RedirectToAction("Index", "OrderLists");
        }

        // GET: NPriEconomyClassFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priEconomyClassFood = await _context.PriEconomyClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priEconomyClassFood == null)
            {
                return NotFound();
            }

            return View(priEconomyClassFood);
        }

        // GET: NPriEconomyClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NPriEconomyClassFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Image,Price,FoodStatus")] PECF pecf)
        {
            PriEconomyClassFood priEconomyClass = new PriEconomyClassFood();



            priEconomyClass.FoodName = pecf.FoodName;
            priEconomyClass.Price = pecf.Price;
            priEconomyClass.FoodStatus = pecf.FoodStatus;



            if (pecf.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, pecf.Image.FileName);



                pecf.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                priEconomyClass.Image = pecf.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(priEconomyClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priEconomyClass);
        }

        // GET: NPriEconomyClassFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priEconomyClassFood = await _context.PriEconomyClassFood.FindAsync(id);
            if (priEconomyClassFood == null)
            {
                return NotFound();
            }
            return View(priEconomyClassFood);
        }

        // POST: NPriEconomyClassFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Image,Price,FoodStatus")] PriEconomyClassFood priEconomyClassFood)
        {
            if (id != priEconomyClassFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priEconomyClassFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriEconomyClassFoodExists(priEconomyClassFood.Id))
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
            return View(priEconomyClassFood);
        }

        // GET: NPriEconomyClassFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priEconomyClassFood = await _context.PriEconomyClassFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priEconomyClassFood == null)
            {
                return NotFound();
            }

            return View(priEconomyClassFood);
        }

        // POST: NPriEconomyClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priEconomyClassFood = await _context.PriEconomyClassFood.FindAsync(id);
            _context.PriEconomyClassFood.Remove(priEconomyClassFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriEconomyClassFoodExists(int id)
        {
            return _context.PriEconomyClassFood.Any(e => e.Id == id);
        }
    }
}
