﻿using System;
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

        public async Task<IActionResult> Status(string PassportNumber)
        {
            EnterPassportNumber = PassportNumber;
            var avaibleFood = await _context.FirstClassFood.Where(m => m.FoodStatus == 1).ToListAsync();

            return View(avaibleFood);
        }

        public async Task<IActionResult> CreateBL([Bind("Id,FoodName,Price,Image,FlightName,Date,Time,SeatNumber")] OrderList orderList)
        {
            int save = 0;
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
                save = await _context.SaveChangesAsync();

                //var bt = _context.BookTickets.Max(B => B.Id);
                //return RedirectToAction("Edit", "BookTickets", new { id = bt });
                if(save > 0)
                {
                    TempData["message"] = "Food Orderd !";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["message"] = "Food Orderd Error !";
                    return RedirectToAction("Index", "Index");
                }
            }
            return View(orderList);
        }
        private bool FirstClassFoodExists(int id)
        {
            return _context.FirstClassFood.Any(e => e.Id == id);
        }
    }
}
