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

namespace AirlineReservation.Controllers
{
    public class RegisterUserController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: RegisterUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
           RegisterUserViewModel  registerUser= new RegisterUserViewModel();
            return View(registerUser);
        }
        [HttpPost]
        public ActionResult Create(RegisterUserViewModel registerUser)
        {
            if (ModelState.IsValid)
            {
                //insert gar
                //converting view model to entity model
                tblUserInformation tblUserInformation = new tblUserInformation();
                tblUserInformation.FirstName = registerUser.FirstName;
                tblUserInformation.LastName = registerUser.LastName;
                tblUserInformation.Email = registerUser.Email;
                tblUserInformation.Password = Cryptography.Encrypt(registerUser.Password);
                tblUserInformation.PhoneNo = registerUser.PhoneNo;
                tblUserInformation.Address = registerUser.Address;
                tblUserInformation.City = registerUser.City;
                tblUserInformation.Country = registerUser.Country;
                tblUserInformation.PostalCode = registerUser.PostalCode;
                tblUserInformation.ProfileImage = registerUser.ProfileImage;
                tblUserInformation.UserTypeID = 3;


                db.tblUserInformations.Add(tblUserInformation);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(registerUser);
        }
    }
}