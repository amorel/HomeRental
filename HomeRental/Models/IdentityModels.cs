using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace HomeRental.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Reservations = new List<Reservation>();
        }
        public string HashMail { get; set; } 
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}