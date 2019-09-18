namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModels1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaOrderItems",
                c => new
                    {
                        PizzaOrderItemId = c.Guid(nullable: false),
                        PizzaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PizzaOrderItemId)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .Index(t => t.PizzaId);
            
            CreateTable(
                "dbo.PizzaOrderItemPizzaOrders",
                c => new
                    {
                        PizzaOrderItem_PizzaOrderItemId = c.Guid(nullable: false),
                        PizzaOrder_OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PizzaOrderItem_PizzaOrderItemId, t.PizzaOrder_OrderId })
                .ForeignKey("dbo.PizzaOrderItems", t => t.PizzaOrderItem_PizzaOrderItemId, cascadeDelete: true)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_OrderId, cascadeDelete: true)
                .Index(t => t.PizzaOrderItem_PizzaOrderItemId)
                .Index(t => t.PizzaOrder_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PizzaOrderItems", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.PizzaOrderItemPizzaOrders", "PizzaOrder_OrderId", "dbo.PizzaOrders");
            DropForeignKey("dbo.PizzaOrderItemPizzaOrders", "PizzaOrderItem_PizzaOrderItemId", "dbo.PizzaOrderItems");
            DropIndex("dbo.PizzaOrderItemPizzaOrders", new[] { "PizzaOrder_OrderId" });
            DropIndex("dbo.PizzaOrderItemPizzaOrders", new[] { "PizzaOrderItem_PizzaOrderItemId" });
            DropIndex("dbo.PizzaOrderItems", new[] { "PizzaId" });
            DropTable("dbo.PizzaOrderItemPizzaOrders");
            DropTable("dbo.PizzaOrderItems");
        }
    }
}
