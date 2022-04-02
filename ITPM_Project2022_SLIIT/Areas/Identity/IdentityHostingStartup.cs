using System;
using ITPM_Project2022_SLIIT.Areas.Identity.Data;
using ITPM_Project2022_SLIIT.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ITPM_Project2022_SLIIT.Areas.Identity.IdentityHostingStartup))]
namespace ITPM_Project2022_SLIIT.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ITPM_Project2022_SLIITDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ITPM_Project2022_SLIITDbContextConnection")));

                services.AddDefaultIdentity<ATMSUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                
                })
                    .AddEntityFrameworkStores<ITPM_Project2022_SLIITDbContext>();
            });
        }
    }
}