using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HomeRental.Models
{
    /// <summary>
    /// Caracteristic of the rental.
    /// </summary>
    public enum PropertyType
    {
        Apartment, House, Chalet
    }
    public class Rental
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int PricePerNight { get; set; }
        public string Address { get; set; }
        public int number { get; set; } 
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? Longitude { get; set; }
        public int? Latitude { get; set; }
        public PropertyType PropertyType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}