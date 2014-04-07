using HomeRental.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HomeRental.DAL
{
    public class RentalContext : DbContext
    {
        public RentalContext() : base("RentalContext")
        {
        }
        
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}