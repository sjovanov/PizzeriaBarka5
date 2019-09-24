namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PicerijaBarka5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Id,
               new IdentityRole { Id = "0", Name = UserRoles.Admin },
               new IdentityRole { Id = "1", Name = UserRoles.User },
               new IdentityRole { Id = "2", Name = UserRoles.Owner }
               );

            var OwnerUser = new ApplicationUser
            {
                UserName = "owner@gmail.com",
                Email = "owner@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var AdminUser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var UserUser = new ApplicationUser
            {
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
            {
                userManager.Create(OwnerUser, "Aa123!");
                userManager.Create(AdminUser, "Aa123!");
                userManager.Create(UserUser, "Aa123!");

                userManager.AddToRole(OwnerUser.Id, UserRoles.Owner);
                userManager.AddToRole(AdminUser.Id, UserRoles.Admin);
                userManager.AddToRole(UserUser.Id, UserRoles.User);
            }

            var Neapolitan_dough = new Ingredient { IngredientId = new Guid("8fdbb911-0c88-4479-8eed-ab7609dda118"), Name = "Neapolitan dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 };
            var Chicago_dough = new Ingredient { IngredientId = new Guid("8fdbb912-0c88-4479-8eed-ab7609dda118"), Name = "Chicago dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 };
            var Sausage_meat = new Ingredient { IngredientId = new Guid("8fdbb913-0c88-4479-8eed-ab7609dda118"), Name = "Sausage", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 100, QuantityInStock = 600 };
            var Beef_meat = new Ingredient { IngredientId = new Guid("8fdbb914-0c88-4479-8eed-ab7609dda118"), Name = "Beef", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 800, QuantityInStock = 600 };
            var Mozzarella_cheese = new Ingredient { IngredientId = new Guid("8fdbb915-0c88-4479-8eed-ab7609dda118"), Name = "Mozzarella", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 350, QuantityInStock = 600 };
            var Parmesan_cheese = new Ingredient { IngredientId = new Guid("8fdbb916-0c88-4479-8eed-ab7609dda118"), Name = "Parmesan", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 320, QuantityInStock = 600 };
            var Tomatillo_sauce = new Ingredient { IngredientId = new Guid("8fdbb917-0c88-4479-8eed-ab7609dda118"), Name = "Tomatillo", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 150, QuantityInStock = 600 };
            var Barbeque_sauce = new Ingredient { IngredientId = new Guid("8fdbb918-0c88-4479-8eed-ab7609dda118"), Name = "Barbecue", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 100, QuantityInStock = 600 };
            var Tomatoes_vegetable = new Ingredient { IngredientId = new Guid("8fdbb919-0c88-4479-8eed-ab7609dda118"), Name = "Tomatoes", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 60, QuantityInStock = 600 };
            var Mushrooms_vegetable = new Ingredient { IngredientId = new Guid("8fdbb920-0c88-4479-8eed-ab7609dda118"), Name = "Mushrooms", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 140, QuantityInStock = 600 };
            var Regular_dough = new Ingredient { IngredientId = new Guid("8fdbb921-0c88-4479-8eed-ab7609dda118"), Name = "Regular dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 };
            var Brioche_dough = new Ingredient { IngredientId = new Guid("8fdbb922-0c88-4479-8eed-ab7609dda118"), Name = "Brioche dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 };
            var Pepperoni_meat = new Ingredient { IngredientId = new Guid("8fdbb923-0c88-4479-8eed-ab7609dda118"), Name = "Pepperoni", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 100, QuantityInStock = 600 };
            var Salami_meat = new Ingredient { IngredientId = new Guid("8fdbb924-0c88-4479-8eed-ab7609dda118"), Name = "Salami", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 800, QuantityInStock = 600 };
            var Cheddar_cheese = new Ingredient { IngredientId = new Guid("8fdbb925-0c88-4479-8eed-ab7609dda118"), Name = "Cheddar", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 350, QuantityInStock = 600 };
            var Gouda_cheese = new Ingredient { IngredientId = new Guid("8fdbb926-0c88-4479-8eed-ab7609dda118"), Name = "Gouda", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 320, QuantityInStock = 600 };
            var Kethcup_sauce = new Ingredient { IngredientId = new Guid("8fdbb927-0c88-4479-8eed-ab7609dda118"), Name = "Ketchup", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 150, QuantityInStock = 600 };
            var Mayonnaise_sauce = new Ingredient { IngredientId = new Guid("8fdbb928-0c88-4479-8eed-ab7609dda118"), Name = "Mayonnaise", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 100, QuantityInStock = 600 };
            var Peppers_vegetable = new Ingredient { IngredientId = new Guid("8fdbb929-0c88-4479-8eed-ab7609dda118"), Name = "Peppers", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 60, QuantityInStock = 600 };
            var Pickles_vegetable = new Ingredient { IngredientId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda118"), Name = "Pickles", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 140, QuantityInStock = 600 };

            context.Ingredients.AddOrUpdate(
                    Neapolitan_dough,
                    Chicago_dough,
                    Sausage_meat,
                    Beef_meat,
                    Mozzarella_cheese,
                    Parmesan_cheese,
                    Tomatillo_sauce,
                    Barbeque_sauce,
                    Tomatoes_vegetable,
                    Mushrooms_vegetable,
                    Regular_dough,
                    Brioche_dough,
                    Pepperoni_meat,
                    Salami_meat,
                    Cheddar_cheese,
                    Gouda_cheese,
                    Kethcup_sauce,
                    Mayonnaise_sauce,
                    Peppers_vegetable,
                    Pickles_vegetable
                );

            var pizza1 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda100"),
                Name = "Capricciosa",
                Ingredients = new List<Ingredient> {
                            Neapolitan_dough,
                            Sausage_meat,
                            Mozzarella_cheese,
                            Tomatillo_sauce,
                            Peppers_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza2 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda101"),
                Name = "Margarita",
                Ingredients = new List<Ingredient> {
                            Chicago_dough,
                            Cheddar_cheese,
                            Gouda_cheese,
                            Tomatillo_sauce
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza3 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda102"),
                Name = "Extra Meaty",
                Ingredients = new List<Ingredient> {
                            Regular_dough,
                            Sausage_meat,
                            Beef_meat,
                            Pepperoni_meat,
                            Tomatillo_sauce,
                            Tomatoes_vegetable,
                            Regular_dough,
                            Mushrooms_vegetable
                    },
                IncomeCoeficient = 1,
                User = OwnerUser
            };
            var pizza4 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda103"),
                Name = "Vegan",
                Ingredients = new List<Ingredient> {
                            Brioche_dough,
                            Tomatoes_vegetable,
                            Mushrooms_vegetable,
                            Peppers_vegetable,
                            Pickles_vegetable,
                            Tomatoes_vegetable
                    },
                IncomeCoeficient = 0.7,
                User = OwnerUser
            };
            var pizza5 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda104"),
                Name = "Neapolitan",
                Ingredients = new List<Ingredient> {
                            Neapolitan_dough,
                            Sausage_meat,
                            Mozzarella_cheese,
                            Cheddar_cheese,
                            Tomatillo_sauce,
                            Tomatoes_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza6 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda105"),
                Name = "Quattro formaggi",
                Ingredients = new List<Ingredient> {
                            Chicago_dough,
                            Mozzarella_cheese,
                            Parmesan_cheese,
                            Cheddar_cheese,
                            Gouda_cheese,
                            Pickles_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza7 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda106"),
                Name = "Half and half",
                Ingredients = new List<Ingredient> {
                            Brioche_dough,
                            Sausage_meat,
                            Beef_meat,
                            Mozzarella_cheese,
                            Gouda_cheese,
                            Tomatillo_sauce,
                            Mushrooms_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza8 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda107"),
                Name = "Pepperoni pizza",
                Ingredients = new List<Ingredient> {
                            Chicago_dough,
                            Pepperoni_meat,
                            Mozzarella_cheese,
                            Tomatillo_sauce,
                            Tomatoes_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza9 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda108"),
                Name = "Sicilian",
                Ingredients = new List<Ingredient> {
                            Neapolitan_dough,
                            Salami_meat,
                            Mozzarella_cheese,
                            Tomatillo_sauce,
                            Pickles_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };
            var pizza10 = new Pizza
            {
                PizzaId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda109"),
                Name = "New York Pizza",
                Ingredients = new List<Ingredient> {
                            Chicago_dough,
                            Sausage_meat,
                            Mozzarella_cheese,
                            Tomatillo_sauce,
                            Tomatoes_vegetable
                    },
                IncomeCoeficient = 0.8,
                User = OwnerUser
            };


            context.Pizzas.AddOrUpdate(
                pizza1,
                pizza2,
                pizza3,
                pizza4,
                pizza5,
                pizza6,
                pizza7,
                pizza8,
                pizza9,
                pizza10
            );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
