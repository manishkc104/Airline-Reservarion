using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Models.BackOffice
{
    public class DestinationViewModel
    {
        public int ID { get; set; }
       
        public string image { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public HttpPostedFileBase FileDestinationLogo { get; set; }
        public List<DestinationModel> DestinationModel { get; set; }

    }

    public class DestinationModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}