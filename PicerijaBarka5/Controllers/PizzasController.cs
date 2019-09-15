using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicerijaBarka5.Models;

namespace PicerijaBarka5.Controllers
{
    public class PizzasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pizzas
        public ActionResult Index()
        {
            return View(db.Pizzas.ToList());
        }

        // GET: Pizzas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizzas/Create
        public ActionResult Create()
        {
            CreatePizzaViewModel createPizzaViewModel = new CreatePizzaViewModel();
            createPizzaViewModel.availableIngredients = db.Ingredients.ToList();
            createPizzaViewModel.selectedIngredients = new List<String>();
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
                PizzaBulder pb = new PizzaBulder();
                var pizza = pb.withName(pizzaResponse.Name)
                                .withIngredients(db.Ingredients.Where(x => pizzaResponse.selectedIngredients.Contains(x.IngredientId.ToString())).ToList())
                                .withIncomeCoef(pizzaResponse.IncomeCoef)
                                .withDough(db.Ingredients.Where(x => x.IngredientId.ToString() == pizzaResponse.Dough).FirstOrDefault())
                                .build();
                pizza.UserFk = User.Identity.GetUserId();
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            pizzaResponse.availableIngredients = db.Ingredients.ToList();
            return View(pizzaResponse);
        }

        // GET: Pizzas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PizzaId,Name,Price")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get: Pizzas/MyPizzas
        public ActionResult MyPizzas()
        {
            ViewBag.test = db.Pizzas.ToList().Where(pizza => pizza.UserFk == User.Identity.GetUserId());  
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
