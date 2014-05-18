using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRental.Models.SubModels
{
    public class RequestSearchAjax
    {
        public Bounds bounds { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? guests { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; } 
    }
}