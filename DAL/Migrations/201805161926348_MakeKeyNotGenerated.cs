namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeKeyNotGenerated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
