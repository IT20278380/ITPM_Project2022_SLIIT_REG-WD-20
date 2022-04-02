using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITPM_Project2022_SLIIT.Models
{
    public class ATMSDbContext : DbContext
    {
        public ATMSDbContext (DbContextOptions<ATMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<ITPM_Project2022_SLIIT.Models.FlightList> FlightList { get; set; }
    }
}
