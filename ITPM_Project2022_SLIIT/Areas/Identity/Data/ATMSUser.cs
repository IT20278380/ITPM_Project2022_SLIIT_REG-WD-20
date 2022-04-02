using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITPM_Project2022_SLIIT.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ATMSUser class
    public class ATMSUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string FullName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }
    }
}
