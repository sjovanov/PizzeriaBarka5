namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PicerijaBarka5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PicerijaBarka5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Roles.AddOrUpdate(x => x.Id,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "0", Name = "Admin" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "1", Name = "User" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "2", Name = "Owner" }
                );
            context.Ingredients.AddOrUpdate(x => x.IngredientId,
                new Models.Ingredient { IngredientId = new Guid("8fdbb911-0c88-4479-8eed-ab7609dda118"), Name = "Neapolitan dough", IngredientType = Models.IngredientTypeEnum.IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb912-0c88-4479-8eed-ab7609dda118"), Name = "Chicago dough", IngredientType = Models.IngredientTypeEnum.IngredientType.Dough, QuantityPerSmallPizza = 250, Price = 30 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb913-0c88-4479-8eed-ab7609dda118"), Name = "Sausage", IngredientType = Models.IngredientTypeEnum.IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 100 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb914-0c88-4479-8eed-ab7609dda118"), Name = "Beef", IngredientType = Models.IngredientTypeEnum.IngredientType.Meat, QuantityPerSmallPizza = 100, Price = 800 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb915-0c88-4479-8eed-ab7609dda118"), Name = "Mozzarella", IngredientType = Models.IngredientTypeEnum.IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 350 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb916-0c88-4479-8eed-ab7609dda118"), Name = "Parmesan", IngredientType = Models.IngredientTypeEnum.IngredientType.Cheese, QuantityPerSmallPizza = 100, Price = 320 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb917-0c88-4479-8eed-ab7609dda118"), Name = "Tomatillo", IngredientType = Models.IngredientTypeEnum.IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 150 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb918-0c88-4479-8eed-ab7609dda118"), Name = "Barbecue", IngredientType = Models.IngredientTypeEnum.IngredientType.Sauce, QuantityPerSmallPizza = 20, Price = 100 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb919-0c88-4479-8eed-ab7609dda118"), Name = "Tomatoes", IngredientType = Models.IngredientTypeEnum.IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 60 },
                new Models.Ingredient { IngredientId = new Guid("8fdbb920-0c88-4479-8eed-ab7609dda118"), Name = "Mushrooms", IngredientType = Models.IngredientTypeEnum.IngredientType.Vegetable, QuantityPerSmallPizza = 100, Price = 140 });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
