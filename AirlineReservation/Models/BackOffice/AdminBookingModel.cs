using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.Models.BackOffice
{
    public class AdminBookingModel
    {
        public int BookId { get; set; }
        public string UserName { get; set; }
        public string BookingDate { get; set; }
    }
}