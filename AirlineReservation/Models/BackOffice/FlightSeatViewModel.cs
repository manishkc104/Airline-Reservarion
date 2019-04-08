using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Models
{
    public class FlightSeatViewModel
    {
        public int ID { get; set; }
        [Required]
        public int FlightID { get; set; }

        public List<SelectListItem> ListFlight { get; set; }
        [Required]
        public int ClassID { get; set; }

        public List<SelectListItem> ListClass { get; set; }
        [Required]
        public int NoOfSeat { get; set; }
    }
}