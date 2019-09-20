using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using PicerijaBarka5.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicerijaBarka5.Services
{
    public class Repository
    {
        private static Repository repository;

        ApplicationDbContext db = new ApplicationDbContext();

        private Repository() { }

        /// <summary>
        /// Returns an instance from Repository used to get data from the database
        /// </summary>
        /// <returns></returns>
        public static Repository GetInstance ()
        {
            if (repository == null)
            {
                repository = new Repository();
            }
            return repository;
        }

        #region PizzaRepository

        public PizzaDto GetPizza (Guid id)
        {
            PizzaDto pizza = db.Pizzas.Find(id).toPizzaDto();

            if (pizza != null)
            {
                return pizza;
            }
            else
            {
                throw new Exception();
            }
        }

        public ICollection<PizzaDto> GetPizzas ()
        {
            return db.Pizzas.ToList()
                            .Select(pizza => pizza.toPizzaDto())
                            .ToList();
        }

        public ICollection<PizzaDto> GetPizzasFromUsersWithRole (Roles role)
        {
            var pizzas = new List<PizzaDto>();
            string stringRole = ((int)role).ToString();
            //IdentityRole ownerRole = db.Roles.Where(x => x.Name == role.ToString()).First();
            return db.Pizzas.Where(pizza => pizza.User.Roles.Any(r => r.RoleId == stringRole))
                            .ToList()
                            .Select(p => p.toPizzaDto()).ToList();
        }

        public ICollection<PizzaDto> GetPizzasFromUser (string userFk)
        {
            return db.Users.Find(userFk)
                            .Pizzas.ToList()
                            .Select(pizza => pizza.toPizzaDto())
                            .ToList();
        }

        public void CreatePizzaForUser(CreatePizzaViewModel pizza, string userFk)
        {
            Pizza dbPizza = new Pizza
            {
                PizzaId = Guid.NewGuid(),
                Name = pizza.Name,
                IncomeCoeficient = pizza.IncomeCoef,
                Ingredients = db.Ingredients.Where(x => pizza.selectedIngredients.Contains(x.IngredientId.ToString()) || x.IngredientId.ToString() == pizza.Dough).ToList(),
                Orders = new List<PizzaOrder>()
            };
            db.Users.Find(userFk).Pizzas.Add(dbPizza);
            db.SaveChanges();
        }

        public void DeletePizza(Guid id)
        {
            Pizza pizza = db.Pizzas.Find(id);

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

        public void UpdatePizza (PizzaDto pizzaDto)
        {
            var dbPizza = db.Pizzas.Find(pizzaDto.PizzaId);

            dbPizza.Name = pizzaDto.Name;
            dbPizza.IncomeCoeficient = pizzaDto.incomeCoeficient;
            
            var dbIngredients = db.Ingredients.Where(ingredient => ingredient.Pizzas.Any(pizza => pizza.PizzaId == pizzaDto.PizzaId)).ToList();
            dbPizza.Ingredients = dbIngredients;

            var dbOrders = db.PizzaOrders.Where(order => order.Items.Any(item => item.PizzaId == pizzaDto.PizzaId)).ToList();
            dbPizza.Orders = dbOrders;

            db.SaveChanges();
        }

        #endregion

        #region IngredientRepository

        public ICollection<IngredientDto> GetIngredients ()
        {
            return db.Ingredients.ToList()
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

        public ICollection<IngredientDto> GetIngredientsForPizza (Guid pizzaId)
        {
            return db.Ingredients.Where(ingredient => ingredient.Pizzas.Any(pizza => pizza.PizzaId == pizzaId))
                                .Select(ingredient => ingredient.toIngredientDto())
                                .ToList();
        }

        public void CreateIngredient (IngredientDto ingredient)
        {
            Ingredient dbIngredient = new Ingredient
            {
                IngredientId = Guid.NewGuid(),
                Name = ingredient.Name,
                IngredientType = ingredient.IngredientType,
                QuantityPerSmallPizza = ingredient.QuantityPerSmallPizza,
                Price = ingredient.Price,
                Pizzas = new List<Pizza>()
            };

            db.Ingredients.Add(dbIngredient);
            db.SaveChanges();
        }

        public void DeleteIngredient (Guid id)
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

        public void UpdateIngredient (IngredientDto ingredient)
        {
            var dbIngredient = db.Ingredients.Find(ingredient.IngredientId);

            dbIngredient.Name = ingredient.Name;
            dbIngredient.IngredientType = ingredient.IngredientType;
            dbIngredient.Price = ingredient.Price;
            dbIngredient.QuantityPerSmallPizza = ingredient.QuantityPerSmallPizza;

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

        public void CreateOrderForUser(Dictionary<string, int> Items, string userFk, string address)
        {
            PizzaOrder dbPizzaOrder = new PizzaOrder
            {
                OrderId = Guid.NewGuid(),
                Address = address,
                OrderStatus = OrderStatus.InProgress
            };

            var orderedPizzas = new List<Pizza>();

            foreach (var orderedItem in Items)
            {
                for (int i = 0; i < orderedItem.Value; i++)
                {
                    Pizza dbPizzaFromOrder = db.Pizzas.Find(new Guid(orderedItem.Key));
                    orderedPizzas.Add(dbPizzaFromOrder);
                }
            }

            dbPizzaOrder.Items = orderedPizzas;

            db.Users.Find(userFk).PizzaOrders.Add(dbPizzaOrder);
            db.PizzaOrders.Add(dbPizzaOrder);
            db.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            PizzaOrder dbPizzaOrder = db.PizzaOrders.Find(id);

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

        public void UpdateOrderStatus(Guid id, OrderStatus newStatus)
        {
            PizzaOrder dbPizzaOrder = db.PizzaOrders.Find(id);

            dbPizzaOrder.OrderStatus = newStatus;

            db.SaveChanges();
        }


        #endregion
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

    }
}