using AirlineReservation.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using AutoMapper;
using AirlineReservation.Filter;
using PagedList;
using PagedList.Mvc;

namespace AirlineReservation.Controllers.BackOffice
{
    [AirlineAuthorization("1", "2")]
    public class FlightSeatController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: FlightSeat
        public ActionResult Index(int? page)
        {
            var flightSeat = db.tblFlightSeats.Include(c => c.tblFlight).Include(c => c.tblClass).ToList().ToPagedList(page ?? 1, 4);
            return View(flightSeat);
        }

        public ActionResult Create()
        {
            FlightSeatViewModel vmFlightSeat = new FlightSeatViewModel();
            vmFlightSeat.ListFlight = (from c in db.tblFlights.ToList()
                                       select new SelectListItem
                                       {
                                           Text = c.FlightNo,
                                           Value = c.ID.ToString()
                                       }).ToList();
            vmFlightSeat.ListClass = (from c in db.tblClasses.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.Class,
                                          Value = c.ID.ToString()
                                      }).ToList();
            return View(vmFlightSeat);
        }

        [HttpPost]
        public ActionResult Create(FlightSeatViewModel vmFlightSeat)
        {
            vmFlightSeat.ListFlight = (from c in db.tblFlights.ToList()
                                       select new SelectListItem
                                       {
                                           Text = c.FlightNo,
                                           Value = c.ID.ToString()
                                       }).ToList();
            vmFlightSeat.ListClass = (from c in db.tblClasses.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.Class,
                                          Value = c.ID.ToString()
                                      }).ToList();
            if (ModelState.IsValid)
            {
                //insert
                //converting view model to entity model
                try
                {
                    tblFlightSeat dbFlightSeat = new tblFlightSeat();
                    dbFlightSeat.FlightID = vmFlightSeat.FlightID;
                    dbFlightSeat.ClassID = vmFlightSeat.ClassID;
                    dbFlightSeat.NoOfSeat = vmFlightSeat.NoOfSeat;

                    db.tblFlightSeats.Add(dbFlightSeat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "seat and class cannot be duplicated");
                }
            }
            return View(vmFlightSeat);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFlightSeat tblFlightSeat = db.tblFlightSeats.Find(id);
            var config = new MapperConfiguration(x => { x.CreateMap<tblFlightSeat, FlightSeatViewModel>(); });
            var _mapper = config.CreateMapper();
            FlightSeatViewModel seat = _mapper.Map<FlightSeatViewModel>(tblFlightSeat);

            seat.ListFlight = (from c in db.tblFlights.ToList()
                               select new SelectListItem
                               {
                                   Text = c.FlightNo,
                                   Value = c.ID.ToString()
                               }).ToList();
            seat.ListClass = (from c in db.tblClasses.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Class,
                                  Value = c.ID.ToString()
                              }).ToList();
            return View(seat);
        }
        [HttpPost]
        public ActionResult Edit(FlightSeatViewModel seat)
        {
            seat.ListFlight = (from c in db.tblFlights.ToList()
                               select new SelectListItem
                               {
                                   Text = c.FlightNo,
                                   Value = c.ID.ToString()
                               }).ToList();
            seat.ListClass = (from c in db.tblClasses.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Class,
                                  Value = c.ID.ToString()
                              }).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    //insert gar
                    //converting view model to entity model
                    var config = new MapperConfiguration
                    (x => { x.CreateMap<FlightSeatViewModel, tblFlightSeat>(); });
                    var _mapper = config.CreateMapper();
                    tblFlightSeat entitymodel = _mapper.Map<tblFlightSeat>(seat);

                    //update database
                    db.Entry(entitymodel).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(seat);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFlightSeat tblFlightSeat = db.tblFlightSeats.Find(id);
            db.tblFlightSeats.Remove(tblFlightSeat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}