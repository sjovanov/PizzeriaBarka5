using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PicerijaBarka5.Models;
using PicerijaBarka5.Services;

namespace PicerijaBarka5.Controllers
{
    public class HomeController : Controller
    {
        Repository repository = Repository.GetInstance();
        public ActionResult Index()
        {
            ViewData["Message"]=getRating().ToString();
            return View(repository.GetMostSold());
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize(Roles = UserRoles.Owner)]
        public ActionResult Contact()
        {
            return View(repository.GetContactFormEntires());
        }

        public double getRating()
        {
            return repository.TotalRating();
        }

        [HttpPost]
        public ActionResult Contact(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                repository.AddContactEntry(form);
                Response.StatusCode = (int)HttpStatusCode.OK;
                ViewBag.Section = "true";
                return Json (new { responseText = "Thank you for reaching out. We will get back to you as soon as possible."}); 
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { responseText = "Please fill all neccessary fields" });
        }

        [HttpPost]
        public ActionResult Delete (Guid id)
        {
            if (id != null)
            {
                repository.DeleteContactEntry(id);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json (new { message = "Contact entry deleted" });
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "There was an error with proccessing your request" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}