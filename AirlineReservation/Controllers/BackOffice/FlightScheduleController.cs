using AirlineReservation.Filter;
using AirlineReservation.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers.BackOffice
{
    [AirlineAuthorization("1", "2")]
    public class FlightScheduleController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: FlightSchedule
        public ActionResult Index(DateTime? departure)
        {
            var departuredate = string.Empty;
            if (departure == null)
            {
                departuredate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                departuredate = departure.Value.ToString("MM/dd/yyyy");
            }

            ViewBag.DepartureDate = departuredate;
            var scheduledFlight = db.tblFlightSchedules.ToList().Where(c => c.DepartureDateTime.ToString("MM/dd/yyyy") == departuredate).ToList();
            return View(scheduledFlight);
        }

        public ActionResult Create()
        {
            FlightScheduleViewModel vmFlightSchedule = new FlightScheduleViewModel();

            vmFlightSchedule.ListFlight = (from c in db.tblFlights.ToList()
                                           select new SelectListItem
                                           {
                                               Text = c.FlightNo,
                                               Value = c.ID.ToString()
                                           }).ToList();
            vmFlightSchedule.ListRoute = (from c in db.tblRoutes.ToList()
                                          select new SelectListItem
                                          {
                                              Text = c.Departure + '-' + c.Arrival,
                                              Value = c.ID.ToString()

                                          }).ToList();
            return View(vmFlightSchedule);
        }
        [HttpPost]
        public ActionResult Create(FlightScheduleViewModel vmFlightSchedule)
        {
            vmFlightSchedule.ListFlight = (from c in db.tblFlights.ToList()
                                           select new SelectListItem
                                           {
                                               Text = c.FlightNo,
                                               Value = c.ID.ToString()
                                           }).ToList();
            vmFlightSchedule.ListRoute = (from c in db.tblRoutes.ToList()
                                          select new SelectListItem
                                          {
                                              Text = c.Departure + '-' + c.Arrival,
                                              Value = c.ID.ToString()
                                          }).ToList();

            if (ModelState.IsValid)
            {
                if (IsValid(vmFlightSchedule))
                {
                    tblFlightSchedule schedule = new tblFlightSchedule();
                    schedule.FlightID = vmFlightSchedule.FlightID;
                    schedule.RouteID = vmFlightSchedule.RouteID;
                    schedule.ArrivalDateTime = vmFlightSchedule.ArrivalDateTime.Value;
                    schedule.DepartureDateTime = vmFlightSchedule.DepartureDateTime.Value;

                    db.tblFlightSchedules.Add(schedule);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(vmFlightSchedule);

        }

        public bool IsValid(FlightScheduleViewModel vmFlightSchedule)
        {
            if (vmFlightSchedule.DepartureDateTime <= DateTime.Now)
            {
                ModelState.AddModelError("", "Departure Date cannot be less than today");
                return false;
            }

            if(vmFlightSchedule.DepartureDateTime > vmFlightSchedule.ArrivalDateTime)
            {
                ModelState.AddModelError("", "Departure Date cannot be greater than arrival date");
                return false;
            }

            var scheduleFlight = db.tblFlightSchedules.ToList().Where(c => c.FlightID == vmFlightSchedule.FlightID && c.DepartureDateTime.ToString("MM/dd/yyyy") == vmFlightSchedule.DepartureDateTime.Value.ToString("MM/dd/yyyy")).ToList();
            if(scheduleFlight.Count > 0)
            {
                ModelState.AddModelError("", "Selected flight already scheduled for this date");
                return false;
            }

            return true;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFlightSchedule tblFlightSchedule = db.tblFlightSchedules.Find(id);
            db.tblFlightSchedules.Remove(tblFlightSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}