namespace PicerijaBarka5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModelsChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "incomeCoeficient", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "incomeCoeficient");
        }
    }
}
