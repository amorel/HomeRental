using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRental.Models.Requests
{
    public class RequestSearchAjax
    {
        public Bounds bounds { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? guests { get; set; } 
    }
}