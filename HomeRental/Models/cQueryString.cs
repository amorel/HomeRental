using System;

namespace HomeRental.Models
{
    public class cQueryString
    {
        public string address { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? guests { get; set; } 
    }
}