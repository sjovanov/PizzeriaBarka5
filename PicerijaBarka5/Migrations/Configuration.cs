namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
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
            //  This method will be called after migrating to the latest version.
            context.Roles.AddOrUpdate(x => x.Id,
               new IdentityRole { Id = "0", Name = UserRoles.Admin },
               new IdentityRole { Id = "1", Name = UserRoles.User },
               new IdentityRole { Id = "2", Name = UserRoles.Owner }
               );
            context.Ingredients.AddOrUpdate(x => x.IngredientId,
                new Ingredient { IngredientId = new Guid("8fdbb911-0c88-4479-8eed-ab7609dda118"), Name = "Neapolitan dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb912-0c88-4479-8eed-ab7609dda118"), Name = "Chicago dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb913-0c88-4479-8eed-ab7609dda118"), Name = "Sausage", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 100, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb914-0c88-4479-8eed-ab7609dda118"), Name = "Beef", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 800, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb915-0c88-4479-8eed-ab7609dda118"), Name = "Mozzarella", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 350, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb916-0c88-4479-8eed-ab7609dda118"), Name = "Parmesan", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 320, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb917-0c88-4479-8eed-ab7609dda118"), Name = "Tomatillo", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 150, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb918-0c88-4479-8eed-ab7609dda118"), Name = "Barbecue", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 100, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb919-0c88-4479-8eed-ab7609dda118"), Name = "Tomatoes", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 60, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb920-0c88-4479-8eed-ab7609dda118"), Name = "Mushrooms", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 140, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb921-0c88-4479-8eed-ab7609dda118"), Name = "Regular dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb922-0c88-4479-8eed-ab7609dda118"), Name = "Brioche dough", IngredientType = IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb923-0c88-4479-8eed-ab7609dda118"), Name = "Pepperoni", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 100, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb924-0c88-4479-8eed-ab7609dda118"), Name = "Salami", IngredientType = IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 800, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb925-0c88-4479-8eed-ab7609dda118"), Name = "Cheddar", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 350, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb926-0c88-4479-8eed-ab7609dda118"), Name = "Gouda", IngredientType = IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 320, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb927-0c88-4479-8eed-ab7609dda118"), Name = "Ketchup", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 150, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb928-0c88-4479-8eed-ab7609dda118"), Name = "Mayonnaise", IngredientType = IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 100, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb929-0c88-4479-8eed-ab7609dda118"), Name = "Peppers", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 60, QuantityInStock = 600 },
                new Ingredient { IngredientId = new Guid("8fdbb930-0c88-4479-8eed-ab7609dda118"), Name = "Pickles", IngredientType = IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 140, QuantityInStock = 600 });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
