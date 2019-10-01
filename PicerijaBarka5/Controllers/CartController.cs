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
    public class CartController : Controller, IDisposable
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

            return View((CartOrderDto)sessionCart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public void AddToCart(Guid id, string size)
        {
            CartOrderDto cart;
            object sessionCart = Session["cart"];

            PizzaDto pizzaToAdd = repository.GetPizza(id);
            pizzaToAdd.Size = size;

            if (sessionCart != null)
            {
                cart = (CartOrderDto)sessionCart;
                if(cart.Items.Any(item => item.Pizza.PizzaId == id && item.Pizza.Size == size))
                {
       
                    cart.Items.Find(x => x.Pizza.PizzaId == id && x.Pizza.Size == size).Quantity++;
                }
                else
                {
                    cart.Items.Add(new CartItemDto
                    {
                        Pizza = pizzaToAdd,
                        Quantity = 1
                    });
                }
                Session["cart"] = cart;
            }
            else
            {
                cart = new CartOrderDto();
                cart.Items.Add(new CartItemDto
                {
                    Pizza = pizzaToAdd,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
        }

        // DELETE: Cart/Delete/id

        public ActionResult RemoveFromCart(Guid id)
        {
            CartOrderDto cart = (CartOrderDto)Session["cart"];

            var pizzaToRemove = cart.Items.First(x => x.Pizza.PizzaId == id);
            pizzaToRemove.Quantity--;
            if (pizzaToRemove.Quantity == 0)
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

        public ActionResult EnterAddress()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}