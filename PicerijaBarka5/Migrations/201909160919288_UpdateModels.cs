namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pizzas", "PizzaOrder_OrderId", "dbo.PizzaOrders");
            DropIndex("dbo.Pizzas", new[] { "PizzaOrder_OrderId" });
            CreateTable(
                "dbo.PizzaOrderItems",
                c => new
                    {
                        PizzaId = c.Guid(nullable: false),
                        PizzaOrder_OrderId = c.Guid(),
                    })
                .PrimaryKey(t => t.PizzaId)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_OrderId)
                .Index(t => t.PizzaId)
                .Index(t => t.PizzaOrder_OrderId);
            
            DropColumn("dbo.Pizzas", "PizzaOrder_OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pizzas", "PizzaOrder_OrderId", c => c.Guid());
            DropForeignKey("dbo.PizzaOrderItems", "PizzaOrder_OrderId", "dbo.PizzaOrders");
            DropForeignKey("dbo.PizzaOrderItems", "PizzaId", "dbo.Pizzas");
            DropIndex("dbo.PizzaOrderItems", new[] { "PizzaOrder_OrderId" });
            DropIndex("dbo.PizzaOrderItems", new[] { "PizzaId" });
            DropTable("dbo.PizzaOrderItems");
            CreateIndex("dbo.Pizzas", "PizzaOrder_OrderId");
            AddForeignKey("dbo.Pizzas", "PizzaOrder_OrderId", "dbo.PizzaOrders", "OrderId");
        }
    }
}
