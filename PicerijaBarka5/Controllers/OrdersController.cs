using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Models;
using PicerijaBarka5.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PicerijaBarka5.Controllers
{
    public class OrdersController : Controller
    {
        private Repository repository = Repository.GetInstance();

        // GET: Orders
        public ActionResult Index()
        {
            return View(repository.GetOrders());
        }

        // GET: Orders/Details/5
        public ActionResult Details(Guid id)
        {
            return View(repository.GetOrder(id));
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(Dictionary<string, int> Items)
        {
            string Address = Request.QueryString["Address"];
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "There was a problem with placing your order. Please try again later" });
            }
            else
            {
                repository.CreateOrderForUser(Items, User.Identity.GetUserId(), Address);
                Response.StatusCode = (int)HttpStatusCode.OK;
                Session["cart"] = null;
                return Json(new { message = "Your order has been placed" });
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
