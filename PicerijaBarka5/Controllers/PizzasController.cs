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

        private ApplicationDbContext db = new ApplicationDbContext();
        public string search = null;
        // GET: Pizzas
        public ActionResult Index(string sortBy)
        {

            ViewBag.SortingName = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            var pizzasSort = db.Pizzas.AsQueryable();
            ICollection<Pizza> pizzas = new List<Pizza>();
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)))
                {
                    foreach (Pizza p in db.Pizzas.ToList())
                    {
                        if (userManager.GetRoles(p.UserFk).Contains(IngredientTypeEnum.Roles.Owner.ToString()))
                        {
                            pizzas.Add(p);
                        }
                    }
                }
            if (!String.IsNullOrEmpty(sortBy))
            {
                System.Diagnostics.Debug.WriteLine(sortBy);
                switch (sortBy)
                {
                    case "Name desc":
                        pizzasSort = pizzasSort.OrderByDescending(s => s.Name);
                        break;
                    case "Name":
                        pizzasSort = pizzasSort.OrderBy(s => s.Name);
                        break;
                    case "Price desc":
                        pizzasSort = pizzasSort.OrderByDescending(s => s.Price);
                        break;
                    case "Price":
                        pizzasSort = pizzasSort.OrderBy(s => s.Price);
                        break;
                    default:
                        pizzasSort = pizzasSort.OrderBy(s => s.Name);
                        break;
                }
                return View(pizzasSort.ToList());
            }
            return View(pizzas);
            
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
            return View(setupCreateOrEditViewModel(null));
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
            foreach (var TypeOfIngredient in Enum.GetValues(typeof(IngredientType)))
            {
                pizzaResponse.TypeIngredientListPairs.Add(TypeOfIngredient.ToString(), repository.GetIngredientsByType((IngredientType)TypeOfIngredient));
            }
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
                return View(setupCreateOrEditViewModel(id));
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
        public ActionResult Edit([Bind(Include = "PizzaId,Name,IncomeCoef,selectedIngredients,TypeIngredientListPairs")] CreatePizzaViewModel pizza)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePizza(pizza);
                return RedirectToAction("Index");
            }

            return View(setupCreateOrEditViewModel(pizza.PizzaId));
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
            ViewBag.Title = "These are your custom pizzas";
            return View("Index", repository.GetPizzasFromUser(User.Identity.GetUserId()));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private CreatePizzaViewModel setupCreateOrEditViewModel(Guid? id)
        {
            CreatePizzaViewModel viewModel = new CreatePizzaViewModel();

            if (id != null)
            {
                var a = id;

                var pizzaToEdit = repository.GetPizza(id);

                viewModel.PizzaId = id;

                foreach (var TypeOfIngredient in Enum.GetValues(typeof(IngredientType)))
                {
                    viewModel.TypeIngredientListPairs.Add(TypeOfIngredient.ToString(), repository.GetIngredientsByType((IngredientType)TypeOfIngredient));
                }

                viewModel.selectedIngredients = repository.GetIngredientsForPizza(id)
                                                        .Select(x => x.IngredientId.ToString())
                                                        .ToList();

                viewModel.Name = pizzaToEdit.Name;
                viewModel.IncomeCoef = pizzaToEdit.incomeCoeficient;
            }
            else
            {
                foreach (var TypeOfIngredient in Enum.GetValues(typeof(IngredientType)))
                {
                    viewModel.TypeIngredientListPairs.Add(TypeOfIngredient.ToString(), repository.GetIngredientsByType((IngredientType)TypeOfIngredient));
                }
            }

            return viewModel;
        }
      
    }
}
