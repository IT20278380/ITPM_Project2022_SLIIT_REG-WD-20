using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;

namespace ITPM_Project2022_SLIIT.Controllers
{
    public class PriEconomyClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;

        public PriEconomyClassFoodsController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: PriEconomyClassFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriEconomyClassFood.ToListAsync());
        }

        public async Task<IActionResult> Status()
        {
            //var FoodStatus = 1;
            var avaibleFood = await _context.PriEconomyClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
        }

        // GET: PriEconomyClassFoods/Details/5
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

        // GET: PriEconomyClassFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriEconomyClassFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Image,Price,FoodStatus")] PriEconomyClassFood priEconomyClassFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priEconomyClassFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priEconomyClassFood);
        }

        // GET: PriEconomyClassFoods/Edit/5
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

        // POST: PriEconomyClassFoods/Edit/5
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

        // GET: PriEconomyClassFoods/Delete/5
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

        // POST: PriEconomyClassFoods/Delete/5
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
