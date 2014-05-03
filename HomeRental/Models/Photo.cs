using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HomeRental.Models
{
    public class Photo
    {
        public int ID { get; set; }
        [Required] 
        public Image image { get; set; }
        [Required]
        public int GrpPhotoId { get; set; } 
    }
}