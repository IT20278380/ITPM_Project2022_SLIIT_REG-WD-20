using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITPM_Project2022_SLIIT.Models;

namespace ITPM_Project2022_SLIIT.Models
{
    public class ATMSDbContext : DbContext
    {
        public ATMSDbContext (DbContextOptions<ATMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<ITPM_Project2022_SLIIT.Models.FlightList> FlightList { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.BookTickets> BookTickets { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.Notification> Notification { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.BookTickets> BookTickets { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.FirstClassFood> FirstClassFood { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.BsClassFood> BsClassFood { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.PriEconomyClassFood> PriEconomyClassFood { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.EconomyClassFood> EconomyClassFood { get; set; }

        public DbSet<ITPM_Project2022_SLIIT.Models.OrderList> OrderList { get; set; }
    }
}
