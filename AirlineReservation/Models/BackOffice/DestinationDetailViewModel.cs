using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Models.BackOffice
{
    public class DestinationDetailViewModel
    {
        public int ID { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int DestinationsID { get; set; }

        public HttpPostedFileBase FileDestinationLogo { get; set; }


        public List<SelectListItem> ListDestination { get; set; }
    }
}