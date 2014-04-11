using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeRental.Models
{
    public class GroupPhoto
    {
        public int ID { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; } 
    }
}