using AirlineReservation.Common;
using AirlineReservation.Models;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirlineReservation.Controllers.BackOffice
{
    public class LoginController : Controller
    {
        AirlineReservationEntities db = new AirlineReservationEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel vmLogin)
        {
            if (ModelState.IsValid)
            {
                var loginInformationList = db.tblUserInformations.ToList().Where(c => (c.UserTypeID == 2 || c.UserTypeID == 1) && 
                c.Email == vmLogin.Email && Cryptography.Decrypt(c.Password) == vmLogin.Password).ToList();
                if (loginInformationList.Count() > 0)
                {
                    var loginInformation = loginInformationList[0];

                    // valid condition authorizing
                    var ticket = new FormsAuthenticationTicket(
                                                           1,
                                                          loginInformation.ID.ToString(),
                                                           DateTime.Now,
                                                           DateTime.Now.Add(FormsAuthentication.Timeout),
                                                           false,
                                                           loginInformation.UserTypeID.ToString());
                    // Encrypt the cookie using the machine key for secure transport
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(
                       FormsAuthentication.FormsCookieName, // Name of auth cookie
                       hash); // Hashed ticket

                    // Set the cookie's expiration time to the tickets expiration time
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                    // Add the cookie to the list for outgoing response
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                }
            }
            return View(vmLogin);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult TroubleLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TroubleLogin(string email)
        {
            var user = db.tblUserInformations.Where(c => c.Email == email).FirstOrDefault();
            if (user != null)
            {
                var subject = "Change Password";
                var toEmail = email;

                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                string changepasswordurl = baseUrl + "/Login/ChangePassword?userid=" + user.ID;
                var body = changepasswordurl;

                MailManagement.SendEmail(subject, toEmail, body);
            }
            else
            {
                ModelState.AddModelError(" ", "Invalid Email");
            }
            return View();
        }

        public ActionResult ChangePassword(int userid)
        {
            ChangePasswordViewModel cp = new ChangePasswordViewModel();
            cp.UserId = userid;
            return View(cp);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel cpViewModel)
        {
            if (ModelState.IsValid)
            {
                tblUserInformation dbUser = db.tblUserInformations.Where(c => c.ID == cpViewModel.UserId).FirstOrDefault();
                if (dbUser != null)
                {
                    dbUser.Password = cpViewModel.Password;
                    db.Entry(dbUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(cpViewModel);
        }
    }
}