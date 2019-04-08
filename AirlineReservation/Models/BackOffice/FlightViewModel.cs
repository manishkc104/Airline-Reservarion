using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirlineReservation.Models
{
    public class FlightViewModel
    {
        public int ID { get; set; }
        [Required]
        public string FlightNo { get; set; }
        public string Detail { get; set; }
        [Required]
        public string FlightName { get; set; }
        public HttpPostedFileBase FileFlightLogo { get; set; }
        public string FlightLogo { get; set; }
    }
}