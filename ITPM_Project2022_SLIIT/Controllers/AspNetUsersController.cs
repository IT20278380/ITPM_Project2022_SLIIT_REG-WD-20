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
    public class AspNetUsersController : Controller
    {
        private ATMSDbContext _context;

        public AspNetUsersController()
        {
        }

        public AspNetUsersController(ATMSDbContext context)
        {
            _context = context;
        }

        // GET: AspNetUsers/Details/5
        public string Details(string UName)
        {
            string userVal = "";

            var userQuery = from user in _context.AspNetUsers select user;
            //var aspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.UserName == a);

            if (!string.IsNullOrEmpty(UName))
            {
                //return NotFound();
                userQuery = userQuery.Where(user => user.UserName.Contains(UName));

                userVal = userQuery.FirstOrDefault().Email;
            }

            return userVal;
        }

        private bool AspNetUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
