using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Models
{
    public class FlightFareViewModel
    {
        public int ID { get; set; }
        [Required]
        public int FlightID { get; set; }
        [Required]
        public int RouteID { get; set; }
        [Required]
        public int ClassID { get; set; }
        [Required]
        public int PassengerTypeID { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int AllowedBaggage { get; set; }
   
        public List<SelectListItem> ListFlight { get; set; }
    
        public List<SelectListItem> ListRoute { get; set; }
       
        public List<SelectListItem> ListClass { get; set; }
     
        public List<SelectListItem> ListPassengerType { get; set; }
    }
}