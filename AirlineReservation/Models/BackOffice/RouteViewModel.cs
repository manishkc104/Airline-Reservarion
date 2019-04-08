using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirlineReservation.Models
{
    public class RouteViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Departure { get; set; }
        [Required]
        public string Arrival { get; set; }
        [Required]
        public string DepartureShort { get; set; }
        [Required]
        public string ArrivalShort { get; set; }
    }
}