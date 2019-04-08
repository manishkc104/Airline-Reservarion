using AirlineReservation.Models;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservation.Controllers
{
    public class HomeController : Controller
    {

        // GET: Destination
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChitwanDestination()
        {
            return View();

        }
        public ActionResult Destination()
        {
            return View();

        }

        public ActionResult Bardiya()
        {
            return View();

        }
        public ActionResult Pokhara()
        {
            return View();

        }
        public ActionResult Kathmandu()
        {
            return View();

        }
        public ActionResult Lumbini()
        {
            return View();

        }
        [HttpPost]
        public ActionResult SendMesage(ContactSendViewModel vmContact)
        {
            var fromEmail = vmContact.email;
            var subject = vmContact.subject;
            var toEmail = ReadConfigData.GetAppSettingsValue("receivingContactEmail");
            var body = "Dear sir, " + vmContact.message;

            MailManagement.SendEmail(subject, toEmail, body, fromEmail);
            return RedirectToAction("Contact", "Home");
        }
    }
}
