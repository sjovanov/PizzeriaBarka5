using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicerijaBarka5.Extentions;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PicerijaBarka5.Services
{
    public class Repository
    {
        private static Repository repository;

        private static ApplicationDbContext db = null;

        private Repository() { }

        public static Repository GetInstance()
        {
            if (repository == null)
            {
                repository = new Repository();
            }
            db = new ApplicationDbContext();
            return repository;
        }

        #region PizzaRepository

        public PizzaDto GetPizza(Guid? id, string userFk)
        {

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                PizzaDto pizza = db.Users.FirstOrDefault(x => x.Id == userFk)
                            .Pizzas.ToList().Where(x => x.PizzaId == id).Select(p => p.toPizzaDto()).FirstOrDefault();
                   
                if (pizza != null)
                {
                    return pizza;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public List<PizzaDto> GetPizzasForIngredient(Guid id)
        {
            return db.Pizzas.Where(x => x.Ingredients.Any(y => y.IngredientId == id))
                            .ToList()
                            .Select(x => x.toPizzaDto())
                            .ToList();
        }

        public ICollection<PizzaDto> GetPizzas()
        {
            return db.Pizzas.ToList()
                            .Select(pizza => pizza.toPizzaDto())
                            .ToList();
        }

        public void DeleteContactEntry(Guid id)
        {
            var entry = db.ContactFormEntries.FirstOrDefault(x => x.ContactId == id);

            db.ContactFormEntries.Remove(entry);

            db.SaveChanges();
        }

        public ICollection<PizzaDto> GetPizzasFromUsersWithRole(string role)
        {
            var pizzas = new List<PizzaDto>();

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                pizzas = db.Pizzas.ToList()
                        .Where(pizza => userManager.IsInRole(pizza.User.Id, role))
                        .Select(x => x.toPizzaDto())
                        .ToList();
            }
            return pizzas;
        }

        public ICollection<PizzaDto> GetPizzasFromUser(string userFk)
        {
            return db.Users.Find(userFk)
                            .Pizzas.ToList()
                            .Select(x => x.toPizzaDto())
                            .ToList();
        }

        public ICollection<PizzaDto> GetSortedPizzasFromUsersWithRole(string role)
        {
            var pizzas = new List<PizzaDto>();

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                pizzas = db.Pizzas.ToList()
                        .Where(pizza => userManager.IsInRole(pizza.User.Id, role))
                        .ToList().OrderBy(x => x.Price)
                        .Select(x => x.toPizzaDto())
                        .ToList();
            }
            return pizzas;
        }

        public ICollection<PizzaDto> GetSortedPizzasFromUsersWithRoleDesc(string role)
        {
            var pizzas = new List<PizzaDto>();

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                pizzas = db.Pizzas.ToList()
                        .Where(pizza => userManager.IsInRole(pizza.User.Id, role))
                        .ToList().OrderByDescending(x => x.Price)
                        .Select(x => x.toPizzaDto())
                        .ToList();
            }
            return pizzas;
        }

        public ICollection<PizzaDto> GetSortedPizzasFromUser(string userFk)
        {
            return db.Users.Find(userFk)
                            .Pizzas.ToList().OrderBy(x => x.Price)
                            .ToList()
                            .Select(x => x.toPizzaDto())
                            .ToList();
        }

        public ICollection<PizzaDto> GetSortedPizzasFromUserDesc(string userFk)
        {
            return db.Users.Find(userFk)
                            .Pizzas.ToList().OrderByDescending(x => x.Price)
                            .ToList()
                            .Select(x => x.toPizzaDto())
                            .ToList();
        }


        public void CreatePizzaForUser(CreatePizzaViewModel pizza, string userFk)
        {
            var ing = db.Ingredients.Where(x => pizza.selectedIngredients.Contains(x.IngredientId.ToString())).ToList();
            Pizza dbPizza = new Pizza
            {
                PizzaId = Guid.NewGuid(),
                Name = pizza.Name,
                IncomeCoeficient = pizza.IncomeCoef,
                Ingredients = ing,
                Price = ing.Select(x => x.toIngredientDto()).Sum(x => x.getPriceForIngredientInSmallPizza()),
                Orders = new List<PizzaOrder>(),
                ImgUrl = pizza.ImgUrl,
                Size = pizza.Size,
                User = db.Users.Find(userFk)
            };

            db.Pizzas.Add(dbPizza);
            db.SaveChanges();
        }

        public void DeletePizza(Guid id)
        {
            Pizza pizza = db.Pizzas.FirstOrDefault(x => x.PizzaId == id);

            if (pizza != null)
            {
                db.Pizzas.Remove(pizza);
                db.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public void UpdatePizza(CreatePizzaViewModel viewModel)
        {
            var dbPizza = db.Pizzas.Find(viewModel.PizzaId);

            dbPizza.Name = viewModel.Name;
            dbPizza.IncomeCoeficient = viewModel.IncomeCoef;
            dbPizza.Size = viewModel.Size;
            db.Entry(dbPizza).Collection(p => p.Ingredients).Load();

            var newIngredients = db.Ingredients.Where(ing => viewModel.selectedIngredients.Contains(ing.IngredientId.ToString())).ToList();
            dbPizza.Ingredients = newIngredients;

            db.SaveChanges();
        }

        public ICollection<PizzaDto> GetMostSold()
        {
            var pizzas = new List<PizzaDto>();

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                pizzas = db.Pizzas.ToList()
                        .Where(pizza => userManager.IsInRole(pizza.User.Id, UserRoles.Owner))
                        .ToList().OrderByDescending(x => x.Price)
                        .Select(x => x.toPizzaDto())
                        .Take(4)
                        .ToList();
            }
            return pizzas;
        }

        #endregion
       
        #region IngredientRepository

        public ICollection<IngredientDto> GetIngredients()
        {
            return db.Ingredients.ToList()
                                .Select(ingredient => ingredient.toIngredientDto())
                                .ToList();
        }

        public ICollection<IngredientDto> GetSortedIngredients()
        {
            return db.Ingredients.ToList()
                                .OrderBy(x => x.Price).ToList()
                                .Select(ingredient => ingredient.toIngredientDto())
                                .ToList();
        }
        public ICollection<IngredientDto> GetSortedIngredientsDesc()
        {
            return db.Ingredients.ToList()
                                .OrderByDescending(x => x.Price).ToList()
                                .Select(ingredient => ingredient.toIngredientDto())
                                .ToList();
        }

        public IngredientDto GetIngredient(Guid id)
        {
            IngredientDto ingredient = db.Ingredients.Find(id).toIngredientDto();

            if (ingredient != null)
            {
                return ingredient;
            }
            else
            {
                throw new Exception();
            }
        }

        public ICollection<IngredientDto> GetIngredientsForPizza(Guid? pizzaId)
        {
            return db.Ingredients.Where(ingredient => ingredient.Pizzas.Any(pizza => pizza.PizzaId == pizzaId))
                                .ToList()
                                .Select(ingredient => ingredient.toIngredientDto())
                                .ToList();
        }

        public ICollection<IngredientDto> GetIngredientsByType(IngredientType type)
        {
            return db.Ingredients.Where(x => x.IngredientType == type)
                                    .ToList()
                                    .Select(x => x.toIngredientDto())
                                    .ToList();
        }

        public void CreateIngredient(IngredientDto ingredient)
        {
            Ingredient dbIngredient = new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                Name = ingredient.Name,
                IngredientType = ingredient.IngredientType,
                QuantityPerSmallPizza = ingredient.QuantityPerSmallPizza,
                Price = ingredient.Price,
                QuantityInStock = ingredient.QuantityInStock
            };
            db.Ingredients.Add(dbIngredient);
            db.SaveChanges();
        }

        public void DeleteIngredient(Guid id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);

            if (ingredient != null)
            {
                db.Ingredients.Remove(ingredient);
                db.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public void UpdateIngredient(IngredientDto ingredient)
        {
            var dbIngredient = db.Ingredients.Find(ingredient.IngredientId);

            dbIngredient.Name = ingredient.Name;
            dbIngredient.IngredientType = ingredient.IngredientType;
            dbIngredient.Price = ingredient.Price;
            dbIngredient.QuantityPerSmallPizza = ingredient.QuantityPerSmallPizza;
            dbIngredient.QuantityInStock = ingredient.QuantityInStock;
            db.SaveChanges();
        }

        #endregion

        #region OrderRepository

        public PizzaOrderDto GetOrder(Guid id)
        {
            PizzaOrderDto order = db.PizzaOrders.Find(id).toOrderDto();

            if (order != null)
            {
                return order;
            }
            else
            {
                throw new Exception();
            }
        }

        public ICollection<PizzaOrderDto> GetOrdersFromUser(string userFk)
        {
            return db.Users.Find(userFk)
                            .PizzaOrders
                            .ToList()
                            .Select(order => order.toOrderDto())
                            .ToList();
        }

        public ICollection<PizzaOrderDto> GetOrders()
        {
            return db.PizzaOrders
                    .ToList()
                    .Select(order => order.toOrderDto())
                    .ToList();
        }

        public void CreateOrder(List<CartItemDto> cartItems, string userFk, string address)
        {
            ApplicationUser user = db.Users.Find(userFk);
            PizzaOrder dbOrder = new PizzaOrder
            {
                OrderId = Guid.NewGuid(),
                Address = address,
                Status = OrderStatus.InProgress,
                User = user,
                TimeOfOrder = DateTime.Now,
                Rating = 0
            };
            foreach (var item in cartItems)
            {
                dbOrder.Items.Add(new CartItem
                {
                    CartItemId = Guid.NewGuid(),
                    Pizza = db.Pizzas.Find(item.Pizza.PizzaId),
                    PizzaSize = item.Pizza.Size,
                    Quantity = item.Quantity
                });
            }

            user.PizzaOrders.Add(dbOrder);
            db.PizzaOrders.Add(dbOrder);
            db.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            PizzaOrder dbPizzaOrder = db.PizzaOrders.Where(x => x.OrderId == id).FirstOrDefault();

            db.Entry(dbPizzaOrder).Collection(x => x.Items).Load();

            if (dbPizzaOrder != null)
            {
                db.PizzaOrders.Remove(dbPizzaOrder);
                db.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public void UpdateOrderStatus(Guid id, string newStatus)
        {
            PizzaOrder dbPizzaOrder = db.PizzaOrders.Find(id);

            dbPizzaOrder.Status = newStatus;

            if (dbPizzaOrder.Status == OrderStatus.Accepted)
            {
                updateOrderIngredients(dbPizzaOrder);
            }

            db.SaveChanges();
        }

        public void RatedOrder(Guid id, int rating)
        {
            PizzaOrder dbPizzaOrder = db.PizzaOrders.Find(id);

            dbPizzaOrder.Rating = rating;
            db.SaveChanges();
        }

        public int TotalRating()
        {
            if (db.PizzaOrders.Count() != 0)
            {
                int sum = db.PizzaOrders.Sum(x => x.Rating);
                int number = db.PizzaOrders.Where(x => x.Rating != 0).Count();
                return sum / number;
            }
            return 0;
        }

        #endregion

        public void AddContactEntry(ContactForm contactForm)
        {
            contactForm.ContactId = Guid.NewGuid();
            contactForm.Time = DateTime.Now;
            db.ContactFormEntries.Add(contactForm);
            db.SaveChanges();
        }

        public IEnumerable<ContactForm> GetContactFormEntires()
        {
            return db.ContactFormEntries.ToList();
        }

        public ContactForm GetContact(Guid id)
        {
            return db.ContactFormEntries.Find(id);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return db.Users.ToList();
        }
        private void updateOrderIngredients(PizzaOrder dbPizzaOrder)
        {
            foreach (var dbItem in dbPizzaOrder.Items)
            {
                foreach (var dbIngredient in dbItem.Pizza.Ingredients)
                {
                    switch (dbItem.PizzaSize)
                    {
                        case PizzaSize.Large:
                            dbIngredient.QuantityInStock -= dbIngredient.QuantityPerSmallPizza * 2.5 * dbItem.Quantity;
                            continue;
                        case PizzaSize.Medium:
                            dbIngredient.QuantityInStock -= dbIngredient.QuantityPerSmallPizza * 1.7 * dbItem.Quantity;
                            continue;
                        case PizzaSize.Small:
                            dbIngredient.QuantityInStock -= dbIngredient.QuantityPerSmallPizza * dbItem.Quantity;
                            continue;

                    }
                }
            }
        }
    }
}