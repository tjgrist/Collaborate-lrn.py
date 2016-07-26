namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtofeed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Feed_Id", c => c.Int());
            AddColumn("dbo.Comments", "Notifications_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Feed_Id");
            CreateIndex("dbo.Comments", "Notifications_Id");
            AddForeignKey("dbo.Comments", "Notifications_Id", "dbo.Notifications", "Id");
            AddForeignKey("dbo.AspNetUsers", "Feed_Id", "dbo.Notifications", "Id");
            DropColumn("dbo.Comments", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Feed_Id", "dbo.Notifications");
            DropForeignKey("dbo.Comments", "Notifications_Id", "dbo.Notifications");
            DropIndex("dbo.Comments", new[] { "Notifications_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Feed_Id" });
            DropColumn("dbo.Comments", "Notifications_Id");
            DropColumn("dbo.AspNetUsers", "Feed_Id");
            DropTable("dbo.Notifications");
            CreateIndex("dbo.Comments", "ApplicationUser_Id");
            AddForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
