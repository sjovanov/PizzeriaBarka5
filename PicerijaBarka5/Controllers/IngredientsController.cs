using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using PicerijaBarka5.Services;

namespace PicerijaBarka5.Controllers
{
    public class IngredientsController : Controller
    {
        private Repository repository = Repository.GetInstance();

        // GET: Ingredients
        public ActionResult Index()
        {
            return View(repository.GetIngredients());
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var ing = repository.GetIngredient(id);
                ing.Pizzas = repository.GetPizzasForIngredient(id);
                return View(ing);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientId,Name,QuantityPerSmallPizza,Price,IngredientType,QuantityInStock")] IngredientDto ingredient)
        {
            if (ModelState.IsValid)
            {
                repository.CreateIngredient(ingredient);
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(repository.GetIngredient(id));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientId,Name,QuantityPerSmallPizza,Price,IngredientType,QuantityInStock")] IngredientDto ingredient)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateIngredient(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            repository.DeleteIngredient(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public ActionResult OrderBy(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            switch (sortOrder)
            {
                case "price_desc":
                    ViewBag.Title = "The ingredients are displayed in descending order";
                    return View("Index", repository.GetSortedIngredientsDesc());
                 
                default:
                    ViewBag.Title = "The ingredients are displayed in ascending order";
                    return View("Index", repository.GetSortedIngredients());
            }
        }
    }
}
