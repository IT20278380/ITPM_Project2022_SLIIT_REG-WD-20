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
    public class PriEconomyClassFoodsController : Controller
    {
        private readonly ATMSDbContext _context;
        private static string EnterPassportNumber;

        public PriEconomyClassFoodsController(ATMSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Status()
        {
            //var FoodStatus = 1;
            var avaibleFood = await _context.PriEconomyClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
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

        private bool PriEconomyClassFoodExists(int id)
        {
            return _context.PriEconomyClassFood.Any(e => e.Id == id);
        }
    }
}
