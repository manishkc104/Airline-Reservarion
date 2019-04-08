using AirlineReservation.Models.BackOffice;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers.BackOffice
{
    public class BookingController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: Booking
        public ActionResult Index()
        {
            var booking = db.tblFlightBooks.ToList();
            var result = (from c in booking
                          select new AdminBookingModel
                          {
                              BookId = c.ID,
                              UserName = GetUserName(c.UserID),
                              BookingDate = GetBookingDate(c.ID)

                          }).ToList();


            return View(result);
        }

        public string GetUserName(int UserId)
        {
            var username = string.Empty;
            var user = db.tblUserInformations.Where(uc => uc.ID == UserId).FirstOrDefault();
            if (user != null)
            {
                username = user.FirstName + " " + user.LastName;
            }
            return username;                
        }

        public string GetBookingDate(int bookId)
        {
            var bookingDate = string.Empty;
            var bookingDetail = db.tblFlightBookDetails.Where(uc => uc.BookID == bookId).FirstOrDefault();
            if (bookingDetail != null)
            {
                bookingDate = bookingDetail.BookingDate.ToShortDateString();
            }
            return bookingDate;
        }

        public ActionResult Delete(int bookId)
        {
            var booking = db.tblFlightBooks.Where(c=>c.ID == bookId).FirstOrDefault();
            if(booking != null)
            {
                db.tblFlightBooks.Remove(booking);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Booking");
        }
    }
}