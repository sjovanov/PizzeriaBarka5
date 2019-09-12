namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModels1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        QuantityPerSmallPizza = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PizzaId);
            
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_PizzaId = c.Guid(nullable: false),
                        Ingredient_IngredientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_PizzaId, t.Ingredient_IngredientId })
                .ForeignKey("dbo.Pizzas", t => t.Pizza_PizzaId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .Index(t => t.Pizza_PizzaId)
                .Index(t => t.Ingredient_IngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_PizzaId", "dbo.Pizzas");
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_PizzaId" });
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Ingredients");
        }
    }
}
