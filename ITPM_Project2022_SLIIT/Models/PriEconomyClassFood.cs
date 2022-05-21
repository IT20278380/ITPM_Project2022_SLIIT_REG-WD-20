using Microsoft.AspNetCore.Http;

namespace ITPM_Project2022_SLIIT.Models
{
    public class PriEconomyClassFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int FoodStatus { get; set; }
    }

    public class PECF
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public IFormFile Image { get; set; }
        public float Price { get; set; }
        public int FoodStatus { get; set; }
    }
}
