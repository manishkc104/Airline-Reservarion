using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Models
{
    public class FlightScheduleViewModel
    {
        public int ID { get; set; }
        [Required]
        public int FlightID { get; set; }
        [Required]
        public int RouteID { get; set; }
        [Required]
        public System.DateTime? DepartureDateTime { get; set; }
        [Required]
        public System.DateTime? ArrivalDateTime { get; set; }

        public List<SelectListItem> ListFlight { get; set; }

        public List<SelectListItem> ListRoute { get; set; }
    }
}