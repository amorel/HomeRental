using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HomeRental.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public int idUser { get; set; }
        public int idRental { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ReservationDBContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
    }
}