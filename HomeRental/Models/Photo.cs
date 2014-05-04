using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HomeRental.Models
{
    public class Photo
    {
        public int ID { get; set; }
        [Required] 
        public string PathImage { get; set; }
        public int RentalId { get; set; }
        public virtual Rental Rental { get; set; }
    }
}