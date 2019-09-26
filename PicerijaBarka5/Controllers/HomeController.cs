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
            return View(repository.GetMostSold());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(Guid id)
        {
            ViewBag.Message = "Your application description page.";

            return View(repository.GetContact(id));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contacts page.";

            return View(repository.GetContactFormEntires());
        }

        [HttpPost]
        public ActionResult Contact(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                repository.AddContactEntry(form);
                Response.StatusCode = (int)HttpStatusCode.OK;
                ViewBag.Section = "true";
                return Json (new { responseText = "OK"}); 
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { responseText = "BAD" });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}