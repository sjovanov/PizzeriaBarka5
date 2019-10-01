using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            ViewBag.Title = "Barka 5's Menu";
            ICollection<PizzaDto> pizzas = new List<PizzaDto>();
            pizzas = repository.GetPizzasFromUsersWithRole(UserRoles.Owner);
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
        public ActionResult Create([Bind(Include = "Name, IncomeCoef, selectedIngredients, Dough, availableIngredients, ImgUrl, Size, UserEmail")] CreatePizzaViewModel pizzaResponse, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(
                Server.MapPath("~/Content/Images"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                pizzaResponse.ImgUrl = "/Content/Images/" + pic;
            }
            else
            {
                pizzaResponse.ImgUrl = "/Content/Images/CustomPizza.png";
            }
            if (ModelState.IsValid)
            {
                repository.CreatePizzaForUser(pizzaResponse, User.Identity.GetUserId());
                if (User.IsInRole(UserRoles.User))
                {
                    ViewBag.Title = "These are your custom pizzas";
                    return RedirectToAction("MyPizzas");
                }
                ViewBag.Title = "Barka 5's Menu";
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
        public ActionResult Edit([Bind(Include = "PizzaId,Name,IncomeCoef,selectedIngredients,TypeIngredientListPairs")] CreatePizzaViewModel pizza, string Url)
        {
            ViewBag.URL = Url;
            if (ModelState.IsValid)
            {
                repository.UpdatePizza(pizza);
                if (ViewBag.URL.ToString().Contains("MyPizzas"))
                    return RedirectToAction("MyPizzas");
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
        public ActionResult DeleteConfirmed(Guid id, string Url)
        {
            ViewBag.URL = Url;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                repository.DeletePizza(id);
                if (ViewBag.URL.ToString().Contains("MyPizzas"))
                    return RedirectToAction("MyPizzas");
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
                viewModel.Size = pizzaToEdit.Size;
                viewModel.ImgUrl = pizzaToEdit.ImgUrl;
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

        public ActionResult OrderBy(string sortOrder, string Url)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.URL = Url;
            switch (sortOrder)
            {
                case "price_desc":
                    ViewBag.Title = "The pizzas are displayed in descending order";
                    if (ViewBag.URL.ToString().Contains("MyPizzas"))
                        return View("Index", repository.GetSortedPizzasFromUserDesc(User.Identity.GetUserId()));
                    else
                        return View("Index", repository.GetSortedPizzasFromUsersWithRoleDesc(UserRoles.Owner));

                default:
                    ViewBag.Title = "The pizzas are displayed in ascending order";
                    if (ViewBag.URL.ToString().Contains("MyPizzas"))
                        return View("Index", repository.GetSortedPizzasFromUser(User.Identity.GetUserId()));
                    else
                        return View("Index", repository.GetSortedPizzasFromUsersWithRole(UserRoles.Owner));
            }

        }

    }
}
