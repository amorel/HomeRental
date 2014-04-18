using System;

namespace HomeRental.Models
{
    public class cQueryString
    {
        public string address { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? guests { get; set; } 

        public String getShortDateCheckin()
        {
            String date = checkin.HasValue ? checkin.Value.Date.ToShortDateString():"";
            return date;
        }
        public String getShortDateCheckout()
        {
            String date = checkout.HasValue ? checkout.Value.Date.ToShortDateString() : "";
            return date;
        }
    }
}