using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(PizzaOrder pizzaOrder)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Cart
        public ActionResult Cart()
        {
            object sessionCart = Session["cart"];
            if (sessionCart != null)
            {
                ViewBag.Valid = "True";
            }
            else
            {
                ViewBag.Valid = "False";
            }

            return View((PizzaOrder)sessionCart);

        }

        // POST: Orders/AddToCart
        public ActionResult AddToCart(Guid id)
        {
            object sessionCart = Session["cart"];
            var pizzaToAdd = db.Pizzas.FirstOrDefault(pizza => pizza.PizzaId == id);

            if (sessionCart != null)
            {
                PizzaOrder cart = (PizzaOrder)sessionCart;
                cart.PizzasToOrder.Add(pizzaToAdd);
            }
            else
            {
                PizzaOrder newPizzaOrder = new PizzaOrder();
                newPizzaOrder.OrderId = Guid.NewGuid();
                newPizzaOrder.UserFk = User.Identity.GetUserId();
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {
                    newPizzaOrder.User = userManager.FindById(newPizzaOrder.UserFk);
                }
                newPizzaOrder.PizzasToOrder.Add(pizzaToAdd);
                Session["cart"] = newPizzaOrder;
            }

            return RedirectToAction("Index", "Pizzas");
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
