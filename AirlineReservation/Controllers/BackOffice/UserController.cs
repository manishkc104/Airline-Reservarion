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
using AirlineReservation.Common;
using PagedList;
using PagedList.Mvc;

namespace AirlineReservation.Controllers
{
    [AirlineAuthorization("1")]
    public class UserController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: User
        public ActionResult Index(int? page)
        {
            var tblUserInformations = db.tblUserInformations.Include(c => c.tblUserType).ToList().ToPagedList(page ?? 1, 4); ;
            return View(tblUserInformations);
        }

        public ActionResult Create()
        {
            UserInformationViewModel userinfo = new UserInformationViewModel();
            return View(userinfo);
        }

        [HttpPost]
        public ActionResult Create(UserInformationViewModel userinfo)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                tblUserInformation tblUserInformation = new tblUserInformation();
                tblUserInformation.FirstName = userinfo.FirstName;
                tblUserInformation.LastName = userinfo.LastName ;
                tblUserInformation.Email = userinfo.Email;
                tblUserInformation.Password = Cryptography.Encrypt(userinfo.Password);
                tblUserInformation.PhoneNo = userinfo.PhoneNo;
                tblUserInformation.Address = userinfo.Address;
                tblUserInformation.City = userinfo.City;
                tblUserInformation.Country = userinfo.Country;
                tblUserInformation.PostalCode = userinfo.PostalCode;
                tblUserInformation.ProfileImage = userinfo.ProfileImage;
                tblUserInformation.UserTypeID = 2;


                db.tblUserInformations.Add(tblUserInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userinfo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUserInformation tblUserInformation = db.tblUserInformations.Find(id);

            var config = new MapperConfiguration(x => { x.CreateMap<tblUserInformation, UserInformationViewModel>(); });
            var _mapper = config.CreateMapper();
            UserInformationViewModel userinfo = _mapper.Map<UserInformationViewModel>(tblUserInformation);
            return View(userinfo);
        }

        [HttpPost]
        public ActionResult Edit(UserInformationViewModel userinfo)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                var config = new MapperConfiguration(x => { x.CreateMap<UserInformationViewModel, tblUserInformation>(); });
                var _mapper = config.CreateMapper();
                tblUserInformation entitymodel = _mapper.Map<tblUserInformation>(userinfo);

                //update database
                db.Entry(entitymodel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");   
            }
            return View(userinfo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUserInformation tblUserInformation = db.tblUserInformations.Find(id);
            db.tblUserInformations.Remove(tblUserInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}