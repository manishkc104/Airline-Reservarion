using AirlineReservation.Filter;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers.BackOffice
{
    [AirlineAuthorization("1","2")]
    public class DashboardController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();

        // GET: Dashboard
        public ActionResult Index()
        {

            ViewBag.flight = db.tblFlights.Count();

            ViewBag.Route = db.tblRoutes.Count();

            ViewBag.Schedules = db.tblFlightSchedules.Count();
            return View();
        }
    }
}