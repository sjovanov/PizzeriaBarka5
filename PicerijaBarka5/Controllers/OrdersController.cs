using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
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
        [Authorize(Roles = UserRoles.Owner + "," + UserRoles.User)]
        public ActionResult Index()
        {
            if (User.IsInRole(UserRoles.Owner))
            {
                return View(repository.GetOrders());
            }
            return View(repository.GetUserOrders(User.Identity.GetUserId()));
        }

        // GET: Orders/Details/5
        [Authorize(Roles = UserRoles.Owner + "," + UserRoles.User)]
        public ActionResult Details(Guid id)
        {
            if(repository.GetUserOrders(User.Identity.GetUserId()).Any(x => x.OrderId == id) || User.IsInRole(UserRoles.Owner))
            {
                return View(repository.GetOrder(id));
            }
            return RedirectToAction("Index");
        }

        // POST: Orders/Create
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public ActionResult Create(List<CartItemDto> Items)
        {
            string Address = Request.QueryString["Address"];
            if ((Items != null && Items.Count <= 0) || (Items == null))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "There was a problem with placing your order. Please try again later" });
            }
            else
            {
                repository.CreateOrder(Items, User.Identity.GetUserId(), Address);
                Response.StatusCode = (int)HttpStatusCode.OK;
                Session["cart"] = null;
                return Json(new { message = "Your order has been placed" });
            }
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
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

        // POST: Orders/Delete/5
        [HttpPost]
        [Authorize(Roles = UserRoles.Owner + "," + UserRoles.User)]
        public ActionResult Delete(Guid id)
        {
            try
            {
                if (repository.GetUserOrders(User.Identity.GetUserId()).Any(x => x.OrderId == id) || User.IsInRole(UserRoles.Owner))
                {
                    repository.DeleteOrder(id);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var a = e.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangeStatus(Guid id, string newStatus)
        {
            repository.UpdateOrderStatus(id, newStatus);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = $"Order status has been successfully changed to '{newStatus}'" });
        }

        [HttpPost]
        public ActionResult GiveRating(Guid id, int rating)
        {
            repository.RatedOrder(id, rating);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { message = $"Your rating has been successfully posted" });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

