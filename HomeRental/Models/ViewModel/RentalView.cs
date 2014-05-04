using HomeRental.Models.SubModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeRental.Models
{
    /// <summary>
    /// Caracteristic of the rental.
    /// </summary>

    public class RentalView
    {
        public int ID { get; set; }
        [Required] 
        public int Capacity { get; set; }
        [Required] 
        [Column("Price per night")]
        public int PricePerNight { get; set; }
        [Required]
        [Column("Property Type")]
        public PropertyType PropertyType { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [Range(0, 9999)]
        public int number { get; set; }
        [Required]
        [Column("Postal Code")]
        [Range(0, 99999)]
        public int PostalCode { get; set; }
        [Required] 
        public string City { get; set; }
        [Required] 
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}