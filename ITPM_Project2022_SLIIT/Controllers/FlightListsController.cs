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
    public class FlightListsController : Controller
    {
        private readonly ATMSDbContext _context;

        public FlightListsController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: FlightLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlightList.ToListAsync());
        }

        // GET: FlightLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightList = await _context.FlightList
                .FirstOrDefaultAsync(m => m.id == id);
            if (flightList == null)
            {
                return NotFound();
            }

            return View(flightList);
        }

        // GET: FlightLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightLists/Create
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

        // GET: FlightLists/Edit/5
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

        // POST: FlightLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FlightName,Date,Time,Destination,FirstClassPrice,BsClassPrice,PriEconomyClassPrice,EconomyClassPrice")] FlightList flightList)
        {
            if (id != flightList.id)
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
                    if (!FlightListExists(flightList.id))
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

        // GET: FlightLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightList = await _context.FlightList
                .FirstOrDefaultAsync(m => m.id == id);
            if (flightList == null)
            {
                return NotFound();
            }

            return View(flightList);
        }

        // POST: FlightLists/Delete/5
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
            return _context.FlightList.Any(e => e.id == id);
        }
    }
}
