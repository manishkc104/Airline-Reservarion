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

namespace AirlineReservation.Controllers
{
    [AirlineAuthorization("1", "2")]
    public class FlightFareController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: FlightFare
        public ActionResult Index(int? page)
        {
            var flightFare = db.tblFares.Include(c => c.tblFlight).Include(c => c.tblClass).Include(c => c.tblRoute).Include(c => c.tblPassengerType).ToList().ToPagedList(page ?? 1, 4);
            return View(flightFare);
        }

        public ActionResult Create()
        {
            FlightFareViewModel vmflightFare = new FlightFareViewModel();
            vmflightFare.ListFlight = (from c in db.tblFlights.ToList()
                                       select new SelectListItem
                                       {
                                           Text = c.FlightNo,
                                           Value = c.ID.ToString()
                                       }).ToList();
            vmflightFare.ListRoute = (from c in db.tblRoutes.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.Departure + '-' + c.Arrival,
                                          Value = c.ID.ToString()

                                      }).ToList();
            vmflightFare.ListPassengerType = (from c in db.tblPassengerTypes.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = c.PassengerType,
                                                  Value = c.ID.ToString()
                                              }).ToList();
            vmflightFare.ListClass = (from c in db.tblClasses.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.Class,
                                          Value = c.ID.ToString()
                                      }).ToList();

            return View(vmflightFare);
        }
        [HttpPost]
        public ActionResult Create(FlightFareViewModel vmflightFare)
        {
            if (ModelState.IsValid)
            {


                tblFare fare = new tblFare();
                fare.FlightID = vmflightFare.FlightID;
                fare.RouteID = vmflightFare.RouteID;
                fare.ClassID = vmflightFare.ClassID;
                fare.PassengerTypeID = vmflightFare.PassengerTypeID;
                fare.Amount = vmflightFare.Amount;
                fare.AllowedBaggage = vmflightFare.AllowedBaggage;

                db.tblFares.Add(fare);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(vmflightFare);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFare tblFare = db.tblFares.Find(id);
            var config = new MapperConfiguration(x => { x.CreateMap<tblFare, FlightFareViewModel>(); });
            var _mapper = config.CreateMapper();
            FlightFareViewModel fare = _mapper.Map<FlightFareViewModel>(tblFare);

            fare.ListFlight = (from c in db.tblFlights.ToList()
                               select new SelectListItem
                               {
                                   Text = c.FlightNo,
                                   Value = c.ID.ToString()
                               }).ToList();
            fare.ListClass = (from c in db.tblClasses.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Class,
                                  Value = c.ID.ToString()
                              }).ToList();
            fare.ListRoute = (from c in db.tblRoutes.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Departure + '-' + c.Arrival,
                                  Value = c.ID.ToString()

                              }).ToList();
            fare.ListPassengerType = (from c in db.tblPassengerTypes.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.PassengerType,
                                          Value = c.ID.ToString()
                                      }).ToList();

            return View(fare);
        }
        [HttpPost]
        public ActionResult Edit(FlightFareViewModel fare)
        {
            fare.ListFlight = (from c in db.tblFlights.ToList()
                               select new SelectListItem
                               {
                                   Text = c.FlightNo,
                                   Value = c.ID.ToString()
                               }).ToList();
            fare.ListClass = (from c in db.tblClasses.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Class,
                                  Value = c.ID.ToString()
                              }).ToList();
            fare.ListRoute = (from c in db.tblRoutes.ToList()
                              select new SelectListItem
                              {
                                  Text = c.Departure + '-' + c.Arrival,
                                  Value = c.ID.ToString()

                              }).ToList();
            fare.ListPassengerType = (from c in db.tblPassengerTypes.ToList()
                                      select new SelectListItem
                                      {
                                          Text = c.PassengerType,
                                          Value = c.ID.ToString()
                                      }).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    //insert gar
                    //converting view model to entity model
                    var config = new MapperConfiguration(x => { x.CreateMap<FlightFareViewModel, tblFare>(); });
                    var _mapper = config.CreateMapper();
                    tblFare entitymodel = _mapper.Map<tblFare>(fare);

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
            return View(fare);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFare tblFlightFare = db.tblFares.Find(id);
            db.tblFares.Remove(tblFlightFare);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}