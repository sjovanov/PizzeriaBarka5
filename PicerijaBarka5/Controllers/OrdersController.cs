using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicerijaBarka5.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.PizzaOrders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(CartOrder orderFromCart)
        {
            string Address = Request.QueryString["Address"];
            if (!ModelState.IsValid)
            {
                return View("Cart/Index");
            }
            else
            {
                PizzaOrder PizzaOrder = new PizzaOrder();

                PizzaOrder.OrderId = Guid.NewGuid();
                PizzaOrder.UserFk = User.Identity.GetUserId();
                PizzaOrder.Address = Address;

                foreach (var pair in orderFromCart.Items)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        PizzaOrder.Items.Add(pair.Key);
                    }
                }

                try
                {
                    db.PizzaOrders.Add(PizzaOrder);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return View("Index", orderFromCart);
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
