using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using AirlineReservation.Models;
using System.Net;
using AutoMapper;
using AirlineReservation.Filter;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace AirlineReservation.Controllers
{
    [AirlineAuthorization("1", "2")]
    public class FlightController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: User
        public ActionResult Index(int? page)
        {
            var tblflight = db.tblFlights.ToList().ToPagedList(page ?? 1, 4);
            return View(tblflight);
        }

        public ActionResult Create()
        {
            FlightViewModel flight = new FlightViewModel();
            return View(flight);
        }

        [HttpPost]
        public ActionResult Create(FlightViewModel flightinfo)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                tblFlight dbFlight = new tblFlight();
                dbFlight.FlightNo = flightinfo.FlightNo;
                dbFlight.Detail = flightinfo.Detail;
                dbFlight.FlightName = flightinfo.FlightName;

                if(flightinfo.FileFlightLogo  != null)
                {
                    var fileName = dbFlight.FlightNo + ".jpg";
                    var path = Server.MapPath("~/FlightImages");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    flightinfo.FileFlightLogo.SaveAs(path + "/" + fileName);
                    dbFlight.FlightLogo = fileName;
                }

                db.tblFlights.Add(dbFlight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flightinfo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFlight dbFlight = db.tblFlights.Find(id);

            FlightViewModel flightinfo = new FlightViewModel();
            flightinfo.ID = dbFlight.ID;
            flightinfo.FlightNo = dbFlight.FlightNo;
            flightinfo.Detail = dbFlight.Detail;
            flightinfo.FlightName = dbFlight.FlightName;
            flightinfo.FlightLogo = dbFlight.FlightLogo;           

            return View(flightinfo);
        }

        [HttpPost]
        public ActionResult Edit(FlightViewModel flightinfo)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                tblFlight dbFlight = new tblFlight();
                dbFlight.ID = flightinfo.ID;
                dbFlight.FlightNo = flightinfo.FlightNo;
                dbFlight.Detail = flightinfo.Detail;
                dbFlight.FlightName = flightinfo.FlightName;

                if (flightinfo.FileFlightLogo != null)
                {
                    var fileName = dbFlight.FlightNo + ".jpg";
                    var path = Server.MapPath("~/FlightImages");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    flightinfo.FileFlightLogo.SaveAs(path + "/" + fileName);
                    dbFlight.FlightLogo = fileName;
                }

                db.Entry(dbFlight).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(flightinfo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFlight dbFlight = db.tblFlights.Find(id);
            db.tblFlights.Remove(dbFlight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}