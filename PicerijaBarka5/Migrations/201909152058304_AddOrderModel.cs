namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaOrders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        UserFk = c.String(maxLength: 128),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFk)
                .Index(t => t.UserFk);
            
            AddColumn("dbo.Pizzas", "PizzaOrder_OrderId", c => c.Guid());
            CreateIndex("dbo.Pizzas", "PizzaOrder_OrderId");
            AddForeignKey("dbo.Pizzas", "PizzaOrder_OrderId", "dbo.PizzaOrders", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PizzaOrders", "UserFk", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pizzas", "PizzaOrder_OrderId", "dbo.PizzaOrders");
            DropIndex("dbo.PizzaOrders", new[] { "UserFk" });
            DropIndex("dbo.Pizzas", new[] { "PizzaOrder_OrderId" });
            DropColumn("dbo.Pizzas", "PizzaOrder_OrderId");
            DropTable("dbo.PizzaOrders");
        }
    }
}
