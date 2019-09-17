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
    public class CartController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
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

        // POST: Cart/AddToCart
        public ActionResult AddToCart(Guid id)
        {
            object sessionCart = Session["cart"];
            var pizzaToAdd = db.Pizzas.FirstOrDefault(pizza => pizza.PizzaId == id);
            pizzaToAdd.PizzaId = id;

            if (sessionCart != null)
            {
                PizzaOrder cart = (PizzaOrder)sessionCart;
                var pizzaInCart = cart.Items.FirstOrDefault(item => item.Pizza.PizzaId == id);

                if (pizzaInCart == default(PizzaOrderItem))
                {
                    cart.Items.Add(new PizzaOrderItem(pizzaToAdd, 1));
                }
                else
                {
                    pizzaInCart.Quantity++;
                }
            }
            else
            {
                PizzaOrder newPizzaOrder = new PizzaOrder();
                newPizzaOrder.OrderId = Guid.NewGuid();
                newPizzaOrder.UserFk = User.Identity.GetUserId();
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)))
                {
                    newPizzaOrder.User = userManager.FindById(newPizzaOrder.UserFk);
                }
                newPizzaOrder.Items.Add(new PizzaOrderItem(pizzaToAdd, 1));
                Session["cart"] = newPizzaOrder;
            }

            return RedirectToAction("Index", "Pizzas");
        }

        // DELETE: Cart/Delete/id

        public ActionResult RemoveFromCart(Guid id)
        {
            object sessionCart = Session["cart"];
            var pizzaToRemove = db.Pizzas.Find(id);
            PizzaOrder cart = (PizzaOrder)sessionCart;

            if (sessionCart != null)
            {
                var pizzaToRemoveInCart = cart.Items.Where(x => x.Pizza.PizzaId == id).FirstOrDefault();
                if(pizzaToRemoveInCart != default(PizzaOrderItem))
                {
                    pizzaToRemoveInCart.Quantity--;
                    if(pizzaToRemoveInCart.Quantity == 0)
                    {
                        cart.Items = cart.Items.Where(x => x.Pizza.PizzaId != id).ToList();
                    }
                } 
                Session["cart"] = cart;
            }
            if (cart.Items.Count == 0)
            {
                Session["cart"] = null;
            }
            return RedirectToAction("Index", "Cart");
        }

    }
}