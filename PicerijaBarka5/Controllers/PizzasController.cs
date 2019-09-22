using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using PicerijaBarka5.Services;

namespace PicerijaBarka5.Controllers
{
    public class PizzasController : Controller
    {
        private Repository repository = Repository.GetInstance();

        // GET: Pizzas
        public ActionResult Index()
        {
            return View(repository.GetPizzasFromUsersWithRole(UserRoles.Owner));
        }

        // GET: Pizzas/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(repository.GetPizza(id));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // GET: Pizzas/Create
        public ActionResult Create()
        {
            CreatePizzaViewModel createPizzaViewModel = new CreatePizzaViewModel();
            createPizzaViewModel.availableIngredients = repository.GetIngredients();
            return View(createPizzaViewModel);
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, IncomeCoef, selectedIngredients, Dough, availableIngredients, UserEmail")] CreatePizzaViewModel pizzaResponse)
        {
            if (ModelState.IsValid)
            {
                repository.CreatePizzaForUser(pizzaResponse, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }

            pizzaResponse.availableIngredients = repository.GetIngredients();
            return View(pizzaResponse);
        }

        // GET: Pizzas/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(repository.GetPizza(id));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PizzaId,Name,Price,Ingredients,User")] PizzaDto pizza)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePizza(pizza);
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(repository.GetPizza(id));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                repository.DeletePizza(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // Get: Pizzas/MyPizzas
        public ActionResult MyPizzas()
        {
            return View("Index", repository.GetPizzasFromUser(User.Identity.GetUserId()));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
