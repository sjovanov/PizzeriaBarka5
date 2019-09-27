namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactForms",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        QuantityPerSmallPizza = c.Int(nullable: false),
                        IngredientType = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        QuantityInStock = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IncomeCoeficient = c.Double(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PizzaId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Pizza_PizzaId = c.Guid(),
                        PizzaOrder_OrderId = c.Guid(),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_PizzaId)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_OrderId)
                .Index(t => t.Pizza_PizzaId)
                .Index(t => t.PizzaOrder_OrderId);
            
            CreateTable(
                "dbo.PizzaOrders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        Address = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        TimeOfOrder = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Pizza_PizzaId = c.Guid(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_PizzaId)
                .Index(t => t.User_Id)
                .Index(t => t.Pizza_PizzaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PizzaOrders", "Pizza_PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pizzas", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PizzaOrders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "PizzaOrder_OrderId", "dbo.PizzaOrders");
            DropForeignKey("dbo.CartItems", "Pizza_PizzaId", "dbo.Pizzas");
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_PizzaId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PizzaOrders", new[] { "Pizza_PizzaId" });
            DropIndex("dbo.PizzaOrders", new[] { "User_Id" });
            DropIndex("dbo.CartItems", new[] { "PizzaOrder_OrderId" });
            DropIndex("dbo.CartItems", new[] { "Pizza_PizzaId" });
            DropIndex("dbo.Pizzas", new[] { "User_Id" });
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PizzaOrders");
            DropTable("dbo.CartItems");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Ingredients");
            DropTable("dbo.ContactForms");
        }
    }
}
