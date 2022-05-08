using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Channels;

namespace ITPM_Project2022_SLIIT.Models
{
    public class EconomyClassFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int FoodStatus { get; set; }
    }

    public class ECF
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public IFormFile Image { get; set; }
        public float Price { get; set; }
        public int FoodStatus { get; set; }
    }
}
