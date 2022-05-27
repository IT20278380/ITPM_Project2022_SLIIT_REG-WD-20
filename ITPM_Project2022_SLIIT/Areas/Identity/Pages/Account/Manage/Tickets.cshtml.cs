using ITPM_Project2022_SLIIT.Areas.Identity.Data;
using ITPM_Project2022_SLIIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ITPM_Project2022_SLIIT.Areas.Identity.Pages.Account.Manage
{
    public class TicketsModel : PageModel
    {
        private readonly ATMSDbContext _context;
        private readonly UserManager<ATMSUser> _userManager;
        private readonly SignInManager<ATMSUser> _signInManager;

        public TicketsModel(
           UserManager<ATMSUser> userManager,
           SignInManager<ATMSUser> signInManager,
           ATMSDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public string PassengerName { get; set; }
        public string PassportNumber { get; set; }
        public string FlightName { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SeatNumber { get; set; }
        public string GateNumber { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private void LoadAsync(BookTickets user)
        {
            PassengerName = user.PassengerName;
            PassportNumber = user.PassportNumber;
            FlightName = user.FlightName;
            Destination = user.Destination;
            Date = user.Date;
            Time = user.Time;
            SeatNumber = user.SeatNumber;
            GateNumber = user.GateNumber;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _context.BookTickets.FirstOrDefaultAsync(m => m.MobileNumber == IndexModel.InputModel.Pnumber);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            LoadAsync(user);
            return Page();
        }

    }
}
