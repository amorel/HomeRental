using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeRental.Models
{
    /// <summary>
    /// Booking a rental by a user.
    /// From StatingDate to EndDate period.
    /// </summary>
    public class Reservation
    {
        public int ID { get; set; }
        [Required]
        [Column("Starting Date")]
        public DateTime StartingDate { get; set; }
        [Required]
        [Column("End Date")]
        public DateTime EndDate { get; set; }
    }
}