using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirlineReservation.Models
{
    public class SearchFlightViewModel
    {
        public string FlightType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Adult { get; set; }
        public string Child { get; set; }
        public Nullable<DateTime> Depart { get; set; }
        public Nullable<DateTime> Return { get; set; }
        public List<FlightDetail> departFlightDetail { get; set; }
        public List<FlightDetail> returnFlightDetail { get; set; }
    }

    public class FlightDetail
    {
        public int FlightId { get; set; }
        public int FlightScheduleId { get; set; }
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        public string FlightLogo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrive { get; set; }
        public string Fare { get; set; }
        public string ChildFare { get; set; }
        public string AllowedBaggage { get; set; }
        public string ClassID { get; set; }
        public string Class { get; set; }

    }

    public class BookFlightViewModel
    {
        public string FlightType { get; set; }
        public string Adult { get; set; }
        public string Child { get; set; }
        public string DepartureAdultFare { get; set; }
        public string DepartureChildFare { get; set; }
        public string DepartureFlight { get; set; }
        public string DepartureFlightClass { get; set; }
        public string ReturnAdultFare { get; set; }
        public string ReturnChildFare { get; set; }
        public string ReturnFlight { get; set; }
        public string ReturnFlightClass { get; set; }
      

        public string jsonPassengerDetail { get; set; }

        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }

    public class PassengerDetailViewModel
    {
        public int ID { get; set; }
        public int PassengerType { get; set; }
        public int BookFlightID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class TicketViewModel
    {
        public string FlightNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PassengerName { get; set; }
        public string Class { get; set; }
        public string PassengerType { get; set; }
        public DateTime DepartureDate { get; set; }
        public int AllowedBaggage { get; set; }
        public int Amount { get; set; }
    }
}