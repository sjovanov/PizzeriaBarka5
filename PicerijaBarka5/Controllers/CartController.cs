using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using PicerijaBarka5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicerijaBarka5.Controllers
{
    public class CartController : Controller
    {

        private Repository repository = Repository.GetInstance();

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

            PizzaDto pizzaToAdd = repository.GetPizza(id);

            if (sessionCart != null)
            {
                cart = (CartOrder)sessionCart;
                ;
                if (cart.Items.ContainsKey(pizzaToAdd))
                {
                    cart.Items[pizzaToAdd]++;
                }
                else
                {
                    cart.Items.Add(pizzaToAdd, 1);
                }
                Session["cart"] = cart;
            }
            else
            {
                cart = new CartOrder();
                cart.Items.Add(pizzaToAdd, 1);
                Session["cart"] = cart;
            }
        }

        // DELETE: Cart/Delete/id
        public ActionResult RemoveFromCart(Guid id)
        {
            CartOrder cart = (CartOrder)Session["cart"];

            var pizzaToRemove = cart.Items.Keys.First(x => x.PizzaId == id);

            cart.Items[pizzaToRemove]--;

            if (cart.Items[pizzaToRemove] == 0)
            {
                cart.Items.Remove(pizzaToRemove);
            }

            Session["cart"] = cart;

            if (cart.Items.Count == 0)
            {
                Session["cart"] = null;
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}