namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PizzaOrders", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Pizzas", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Pizzas", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.PizzaOrders", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PizzaOrders", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Pizzas", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Pizzas", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.PizzaOrders", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
