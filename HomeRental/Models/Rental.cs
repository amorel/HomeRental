using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HomeRental.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int PricePerNight { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }

    public class RentalDBContext : DbContext
    {
        public DbSet<Rental> Rentals { get; set; }
    }
}