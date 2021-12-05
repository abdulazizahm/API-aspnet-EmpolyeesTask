namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "User_ID", "dbo.User");
            DropIndex("dbo.Employees", new[] { "User_ID" });
            DropColumn("dbo.Employees", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "User_ID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "User_ID");
            AddForeignKey("dbo.Employees", "User_ID", "dbo.User", "Id");
        }
    }
}
