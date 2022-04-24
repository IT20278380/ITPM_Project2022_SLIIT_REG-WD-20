namespace ITPM_Project2022_SLIIT.Models
{
    public class FlightList
    {
        public int Id { get; set; }
        public string FlightName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Destination { get; set; }
        public string FirstClassPrice { get; set; }
        public string BsClassPrice { get; set; }
        public string PriEconomyClassPrice { get; set; }
        public string EconomyClassPrice { get; set; }

    }
}
