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

        private ApplicationDbContext db = new ApplicationDbContext();
        public string search = null;
        // GET: Pizzas
        public ActionResult Index()
        {
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
        public ActionResult Create([Bind(Include = "Name, IncomeCoef, selectedIngredients, Dough, availableIngredients, ImgUrl, UserEmail")] CreatePizzaViewModel pizzaResponse, HttpPostedFileBase file)
        {
           
            if (file != null)
            {
                System.Diagnostics.Debug.WriteLine("file");
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Images"), pic);
                // file is uploaded
                file.SaveAs(path);
                System.Diagnostics.Debug.WriteLine(path);

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
     /*   public ActionResult FileUpload(HttpPostedFileBase file)
        {
           
            // after successfully uploading redirect the user
            return View();
        }*/

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
    
        public ActionResult OrderBy(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            switch (sortOrder)
            {
                case "price_desc":
                    ViewBag.Title = "The pizzas are displayed in descending order";
                    return View("Index", repository.getSortedPizzasDesc());

                default:
                    ViewBag.Title = "The pizzas are displayed in ascending order";
                    return View("Index", repository.getSortedPizzas());
                 
                
                    
            }
            
        }
      
    }
}
