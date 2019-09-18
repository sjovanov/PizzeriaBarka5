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

            return View((CartOrder)sessionCart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public void AddToCart(Guid id)
        {
            CartOrder cart;
            object sessionCart = Session["cart"];

            if (sessionCart != null)
            {
                cart = (CartOrder)sessionCart;
                var pizzaInCart = cart.Items.First(item => item.Key.PizzaId == id);
                if (pizzaInCart.Key != null)
                {
                    ;
                }
                else
                {
                    cart.Items.Add(pizzaToAdd, 1);
                }
            }
            else
            {
                cart = new CartOrder();
                cart.Items.Add(pizzaToAdd, 1);
                Session["cart"] = cart;
            }
        }

        // DELETE: Cart/Delete/id
        [HttpDelete]
        public ActionResult RemoveFromCart(Guid id)
        {
            CartOrder cart = null;
            object sessionCart = Session["cart"];
            var pizzaToRemove = db.Pizzas.Find(id);

            if (sessionCart != null)
            {
                cart = (CartOrder)sessionCart;
                if (cart.Items.ContainsKey(pizzaToRemove))
                {
                    cart.Items[pizzaToRemove]--;
                    if(cart.Items[pizzaToRemove] == 0)
                    {
                        cart.Items.Remove(pizzaToRemove);
                    }
                }
                Session["cart"] = cart;
            }
            if (cart != null && cart.Items.Count == 0)
            {
                Session["cart"] = null;
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}