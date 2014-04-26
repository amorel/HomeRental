using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRental.Models.Requests
{
    public class Bounds
    {
        public LatLng latlng { get; set; }
        public LatLng jlatlng { get; set; } 
    }
}