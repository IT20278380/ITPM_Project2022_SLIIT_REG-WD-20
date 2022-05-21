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
    public class NFirstClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;

        private IWebHostEnvironment _env;
        public NFirstClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Main View
        public IActionResult MainView()
        {
            return View();
        }

        // GET: NFirstClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.FirstClassFood.ToListAsync());
        }

        // GET: NFirstClassFoods/Details/5
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

        // GET: NFirstClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NFirstClassFoods/Create
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


        // GET: NFirstClassFoods/Edit/5
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

        // POST: NFirstClassFoods/Edit/5
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

        // GET: NFirstClassFoods/Delete/5
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

        // POST: NFirstClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firstClassFood = await _context.FirstClassFood.FindAsync(id);
            _context.FirstClassFood.Remove(firstClassFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirstClassFoodExists(int id)
        {
            return _context.FirstClassFood.Any(e => e.Id == id);
        }
    }
}
