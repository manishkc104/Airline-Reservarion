using AirlineReservation.Models;
using AutoMapper;
using Common;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers
{
    public class SearchFlightController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: SearchFlight
        public ActionResult Index(SearchFlightViewModel searchFlightViewModel)
        {
            var result = (from f in db.tblFlights.ToList()
                          join fs in db.tblFlightSchedules.ToList() on f.ID equals fs.FlightID
                          join r in db.tblRoutes.ToList() on fs.RouteID equals r.ID
                          join ff in db.tblFares.ToList() on new { flightId = f.ID, routeId = r.ID, passengertype = 2 } equals new { flightId = ff.FlightID, routeId = ff.RouteID, passengertype = ff.PassengerTypeID }
                          join c in db.tblClasses on ff.ClassID equals c.ID
                          join fseat in db.tblFlightSeats on new {flightId = f.ID, classid= c.ID } equals new { flightId = fseat.FlightID, classid = fseat.ClassID }
                          where GetBookedSeat(fs.ID, c.ID) < fseat.NoOfSeat
                          select new FlightDetail
                          {
                              FlightId = f.ID,
                              FlightScheduleId = fs.ID,
                              FlightNo = f.FlightNo,
                              FlightName = f.FlightName,
                              FlightLogo = f.FlightLogo,
                              From = r.Departure,
                              To = r.Arrival,
                              Depart = fs.DepartureDateTime,
                              Arrive = fs.ArrivalDateTime,
                              Fare = ff.Amount.ToString(),
                              ChildFare = GetChildFare(f.ID, c.ID, r.ID),
                              AllowedBaggage = ff.AllowedBaggage.ToString(),
                              ClassID = c.ID.ToString(),
                              Class = c.Class
                          }).ToList();

            var departFlightListDetail = new List<FlightDetail>();
            var returnFlightListDetail = new List<FlightDetail>();

            //depart flight List
            departFlightListDetail = result;
            if (!string.IsNullOrEmpty(searchFlightViewModel.From))
            {
                departFlightListDetail = departFlightListDetail.Where(r => r.From.ToLower().Contains(searchFlightViewModel.From.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchFlightViewModel.To))
            {
                departFlightListDetail = departFlightListDetail.Where(r => r.To.ToLower().Contains(searchFlightViewModel.To.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchFlightViewModel.Depart.Value.ToLongDateString()))
            {
                departFlightListDetail = departFlightListDetail.Where(r => r.Depart.ToShortDateString() == searchFlightViewModel.Depart.Value.ToShortDateString()).ToList();
            }
            searchFlightViewModel.departFlightDetail = departFlightListDetail;

            //return flightList
            if (!string.IsNullOrEmpty(searchFlightViewModel.FlightType) && searchFlightViewModel.FlightType == "2")
            {
                returnFlightListDetail = result;
                if (!string.IsNullOrEmpty(searchFlightViewModel.From))
                {
                    returnFlightListDetail = returnFlightListDetail.Where(r => r.From.ToLower().Contains(searchFlightViewModel.To.Trim().ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(searchFlightViewModel.To))
                {
                    returnFlightListDetail = returnFlightListDetail.Where(r => r.To.ToLower().Contains(searchFlightViewModel.From.Trim().ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(searchFlightViewModel.Depart.Value.ToLongDateString()) && !string.IsNullOrEmpty(searchFlightViewModel.Return.Value.ToLongDateString()))
                {
                    returnFlightListDetail = returnFlightListDetail.Where(r => r.Depart.ToShortDateString() == searchFlightViewModel.Return.Value.ToShortDateString()).ToList();
                }
            }
            searchFlightViewModel.returnFlightDetail = returnFlightListDetail;


            //join ff in db.tblFares.ToList() on f.ID equals ff.FlightID && ff.RouteID equals r.ID

            return View(searchFlightViewModel);
        }


        public int GetBookedSeat(int flightscheduleid, int classid)
        {
            var bookedseat = db.tblFlightBookDetails.GroupBy(g => new { g.FlightScheduleID, g.ClassID })
                .Select(c => new { FlightScheduleId = c.Key.FlightScheduleID, ClassId = c.Key.ClassID, Count = c.Count() }).ToList();
            var result = bookedseat.Where(c => c.ClassId == classid && c.FlightScheduleId == flightscheduleid).Select(c => c.Count).FirstOrDefault();
            return result;
        }

        public string GetChildFare(int flightId, int classid, int routeId)
        {
            string childfare = 0.ToString();
            var flightFare = db.tblFares.Where(c => c.FlightID == flightId && c.ClassID == classid && c.RouteID == routeId && c.PassengerTypeID == 1).FirstOrDefault();
            if (flightFare != null)
            {
                childfare = flightFare.Amount.ToString();
            }
            return childfare;
        }

        public ActionResult Search(SearchFlightViewModel searchFlightViewModel)
        {
            return RedirectToAction("Index", "SearchFlight", searchFlightViewModel);
        }

        //[HttpPost]
        //public ActionResult Book(BookFlightViewModel bookDetail)
        //{
        //    //BookFlightViewModel bfVm = JsonConvert.DeserializeObject<BookFlightViewModel>(bookingDetail);
        //    //return View(bookingDetail);
        //    return RedirectToAction("Booking", "SearchFlight", new { bookingDetail = bookDetail });
        //}

        public ActionResult Booking(string bookingDetail)
        {
            BookFlightViewModel bfVm = JsonConvert.DeserializeObject<BookFlightViewModel>(bookingDetail);
            var authenticatedUser = Common.Common.GetAuthenticatedUser();
            if (authenticatedUser > 0)
            {
                var userinfo = db.tblUserInformations.Where(c => c.ID == authenticatedUser).FirstOrDefault();
                bfVm.ID = userinfo.ID;
                bfVm.FirstName = userinfo.FirstName;
                bfVm.LastName = userinfo.LastName;
                bfVm.Email = userinfo.Email;
                bfVm.PhoneNo = userinfo.PhoneNo;
                bfVm.Address = userinfo.Address;
                bfVm.City = userinfo.City;
                bfVm.Country = userinfo.Country;
                bfVm.PostalCode = userinfo.PostalCode;
            }
            return View(bfVm);
        }
        [HttpPost]
        public ActionResult Booking(BookFlightViewModel bfVm)
        {
            var user = new tblUserInformation();
            if (bfVm.ID > 0)
            {
                user = db.tblUserInformations.Where(c => c.ID == bfVm.ID).FirstOrDefault();
            }
            else
            {
                user.FirstName = bfVm.FirstName;
                user.LastName = bfVm.LastName;
                user.Email = bfVm.Email;
                user.Password = "dummy";
                user.UserTypeID = 4;
                user.PhoneNo = bfVm.PhoneNo;
                user.Address = bfVm.Address;
                user.City = bfVm.City;
                user.Country = bfVm.Country;
                user.PostalCode = bfVm.PostalCode;
                db.tblUserInformations.Add(user);
                db.SaveChanges();
            }

            var flightBook = new tblFlightBook();
            flightBook.UserID = user.ID;
            flightBook.TripTypeID = Convert.ToInt32(bfVm.FlightType);
            db.tblFlightBooks.Add(flightBook);
            db.SaveChanges();

            List<PassengerDetailViewModel> passengerDetailList = JsonConvert.DeserializeObject<List<PassengerDetailViewModel>>(bfVm.jsonPassengerDetail);

            //departure
            foreach (var pdetail in passengerDetailList)
            {
                var flightBookDetail = new tblFlightBookDetail();
                flightBookDetail.BookID = flightBook.ID;
                flightBookDetail.FlightScheduleID = Convert.ToInt32(bfVm.DepartureFlight);
                flightBookDetail.ClassID = Convert.ToInt32(bfVm.DepartureFlightClass);
                flightBookDetail.BookingDate = DateTime.Now;
                db.tblFlightBookDetails.Add(flightBookDetail);
                db.SaveChanges();

                var passengerDetail = new tblPassengerDetail();
                passengerDetail.BookFlightID = flightBookDetail.ID;
                passengerDetail.PassengerType = pdetail.PassengerType;
                passengerDetail.FirstName = pdetail.FirstName;
                passengerDetail.LastName = pdetail.LastName;
                passengerDetail.Gender = pdetail.Gender;
                passengerDetail.Phone = pdetail.Phone;
                passengerDetail.Address = pdetail.Address;
                db.tblPassengerDetails.Add(passengerDetail);
                db.SaveChanges();

                if (bfVm.FlightType == "2")
                {
                    var returnflightBookDetail = new tblFlightBookDetail();
                    returnflightBookDetail.BookID = flightBook.ID;
                    returnflightBookDetail.FlightScheduleID = Convert.ToInt32(bfVm.ReturnFlight);
                    returnflightBookDetail.ClassID = Convert.ToInt32(bfVm.ReturnFlightClass);
                    returnflightBookDetail.BookingDate = DateTime.Now;
                    db.tblFlightBookDetails.Add(returnflightBookDetail);
                    db.SaveChanges();

                    var returnpassengerDetail = new tblPassengerDetail();
                    returnpassengerDetail.BookFlightID = returnflightBookDetail.ID;
                    returnpassengerDetail.PassengerType = pdetail.PassengerType;
                    returnpassengerDetail.FirstName = pdetail.FirstName;
                    returnpassengerDetail.LastName = pdetail.LastName;
                    returnpassengerDetail.Gender = pdetail.Gender;
                    returnpassengerDetail.Phone = pdetail.Phone;
                    returnpassengerDetail.Address = pdetail.Address;
                    db.tblPassengerDetails.Add(returnpassengerDetail);
                    db.SaveChanges();
                }
            }



            return RedirectToAction("FlightTicket", "SearchFlight", new { bookId = flightBook.ID });
        }
        public ActionResult FlightTicket(int bookId)
        {
            //TicketViewModel
            var result = (from fbd in db.tblFlightBookDetails.ToList()
                          join pd in db.tblPassengerDetails.ToList() on fbd.ID equals pd.BookFlightID
                          join fb in db.tblFlightBooks.ToList() on fbd.BookID equals fb.ID
                          join fs in db.tblFlightSchedules.ToList() on fbd.FlightScheduleID equals fs.ID
                          join f in db.tblFlights.ToList() on fs.FlightID equals f.ID
                          join r in db.tblRoutes.ToList() on fs.RouteID equals r.ID
                          join c in db.tblClasses on fbd.ClassID equals c.ID
                          join pt in db.tblPassengerTypes on pd.PassengerType equals pt.ID
                          join ff in db.tblFares.ToList() on new { flightId = f.ID, classId = c.ID, passengerTypeId = pt.ID, routeId = r.ID } equals new { flightId = ff.FlightID, classId = ff.ClassID, passengerTypeId = ff.PassengerTypeID, routeId = ff.RouteID }
                          where fb.ID == bookId
                          select new TicketViewModel
                          {
                              FlightNumber = f.FlightNo,
                              From = r.DepartureShort,
                              To = r.ArrivalShort,
                              PassengerName = pd.FirstName + " " + pd.LastName,
                              Class = c.Class,
                              PassengerType = pt.PassengerType,
                              DepartureDate = fs.DepartureDateTime,
                              AllowedBaggage = ff.AllowedBaggage,
                              Amount = ff.Amount
                          }).ToList();

            var userId = db.tblFlightBooks.Where(c => c.ID == bookId).FirstOrDefault().UserID;
            ViewBag.ToEmail = db.tblUserInformations.Where(c => c.ID == userId).FirstOrDefault().Email;
            return View(result);
        }

        [HttpPost]
        public ActionResult SendTicket(string toemail, string Content)
        {
            var subject = "Ticket";
            var toEmail = toemail;
            var body = HttpUtility.UrlDecode(Content);

            MailManagement.SendEmail(subject, toEmail, body);
            return new EmptyResult();
        }
    }
}