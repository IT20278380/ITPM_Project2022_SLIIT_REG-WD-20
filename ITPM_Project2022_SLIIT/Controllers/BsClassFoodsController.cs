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

        public async Task<IActionResult> Status(string PassportNumber)
        {
            //var FoodStatus = 1;
            EnterPassportNumber = PassportNumber;
            var avaibleFood = await _context.BsClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
        }

        public async Task<IActionResult> order()
        {
            return RedirectToAction("Index", "OrderLists");
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
