using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRental.Models.Requests
{
    public class Bounds
    {
        public LatLng northEastLatLng { get; set; }
        public LatLng southWestLatLng { get; set; } 
    }
}