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
    public class NBsClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private IWebHostEnvironment _env;
        public NBsClassFoodsController(ATMSDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: NBsClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.BsClassFood.ToListAsync());
        }

        // GET: NBsClassFoods/Details/5
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

        // GET: NBsClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NBsClassFoods/Create
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



        // GET: NBsClassFoods/Edit/5
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

        // POST: NBsClassFoods/Edit/5
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

        // GET: NBsClassFoods/Delete/5
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

        // POST: NBsClassFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bsClassFood = await _context.BsClassFood.FindAsync(id);
            _context.BsClassFood.Remove(bsClassFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BsClassFoodExists(int id)
        {
            return _context.BsClassFood.Any(e => e.Id == id);
        }
    }
}
