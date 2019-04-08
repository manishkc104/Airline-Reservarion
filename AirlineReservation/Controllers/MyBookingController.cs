using AirlineReservation.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers
{
    public class MyBookingController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: MyBooking
        public ActionResult Index()
        {
            var userId = Common.Common.GetAuthenticatedUser();

            var booking = db.tblFlightBooks.Where(c=>c.UserID == userId).ToList();
            var result = (from c in booking
                          select new BookingViewModel
                          {
                              BookId = c.ID,
                              BookingDate = GetBookingDate(c.ID)

                          }).ToList();


            return View(result);
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
            var booking = db.tblFlightBooks.Where(c => c.ID == bookId).FirstOrDefault();
            if (booking != null)
            {
                db.tblFlightBooks.Remove(booking);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "MyBooking");
        }
    }
}