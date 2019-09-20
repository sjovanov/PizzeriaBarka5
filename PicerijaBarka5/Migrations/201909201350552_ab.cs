namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ab : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pizzas", "UserFk");
            DropColumn("dbo.PizzaOrders", "UserFk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PizzaOrders", "UserFk", c => c.String());
            AddColumn("dbo.Pizzas", "UserFk", c => c.String(nullable: false));
        }
    }
}
