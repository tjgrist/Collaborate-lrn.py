namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationsfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "ApplicationUser_Id");
            AddForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Comments", "ApplicationUser_Id");
        }
    }
}
