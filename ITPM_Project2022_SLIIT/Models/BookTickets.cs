using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Channels;

namespace ITPM_Project2022_SLIIT.Models
{
    public class BookTickets
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public string PassportNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string FlightName { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Class { get; set; }
        public string RequiredSeat { get; set; }
        public string PaidPrice { get; set; }
        public string SeatNumber { get; set; }
        public string GateNumber { get; set; }
        public string TicketNumber { get; set; }
        public string PaidRecipt { get; set; }
    }

    public class BT
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public string PassportNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string FlightName { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Class { get; set; }
        public string RequiredSeat { get; set; }
        public string PaidPrice { get; set; }
        public string SeatNumber { get; set; }
        public string GateNumber { get; set; }
        public string TicketNumber { get; set; }
        public IFormFile PaidRecipt { get; set; }
    }
}

