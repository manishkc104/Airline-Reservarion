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
using PagedList;
using PagedList.Mvc;

namespace AirlineReservation.Controllers.BackOffice
{
    [AirlineAuthorization("1", "2")]
    public class RouteController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();

        // GET: Route
        public ActionResult Index(int? page)
        {
            var tblRoute = db.tblRoutes.ToList().ToPagedList(page ?? 1, 4);
            return View(tblRoute);
        }
        public ActionResult Create()
        {
           RouteViewModel route = new RouteViewModel();
            return View(route);      
        }
        [HttpPost]
        public ActionResult Create(RouteViewModel route)
        {
            if (ModelState.IsValid)
            {
                tblRoute tblRoute = new tblRoute();
                tblRoute.Departure = route.Departure;
                tblRoute.Arrival = route.Arrival;
                tblRoute.DepartureShort = route.DepartureShort;
                tblRoute.ArrivalShort = route.ArrivalShort;


                db.tblRoutes.Add(tblRoute);
                db.SaveChanges();

                return RedirectToAction("Index");


            }
            return View(route);
        }
        public ActionResult Edit(int? id = null)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRoute tblRoute = db.tblRoutes.Find(id);
            var config = new MapperConfiguration(x => { x.CreateMap<tblRoute, RouteViewModel>(); });
            var _mapper = config.CreateMapper();
            RouteViewModel route = _mapper.Map<RouteViewModel>(tblRoute);
            return View(route);
        }
        [HttpPost]
        public ActionResult Edit(RouteViewModel route)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                var config = new MapperConfiguration(x => { x.CreateMap<RouteViewModel, tblRoute>(); });
                var _mapper = config.CreateMapper();
                tblRoute entitymodel = _mapper.Map<tblRoute>(route);

                //update database
                db.Entry(entitymodel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(route);
        }
        public ActionResult Delete(int? id)
        {

            tblRoute tblRoute = db.tblRoutes.Find(id);
            db.tblRoutes.Remove(tblRoute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}