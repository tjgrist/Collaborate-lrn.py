namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removednotificationschanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Notifications_Id", "dbo.Notifications");
            DropForeignKey("dbo.AspNetUsers", "Feed_Id", "dbo.Notifications");
            DropForeignKey("dbo.Comments", "CollaborativeTutorialId", "dbo.CollaborativeTutorials");
            DropIndex("dbo.AspNetUsers", new[] { "Feed_Id" });
            DropIndex("dbo.Comments", new[] { "CollaborativeTutorialId" });
            DropIndex("dbo.Comments", new[] { "Notifications_Id" });
            RenameColumn(table: "dbo.Comments", name: "CollaborativeTutorialId", newName: "CollaborativeTutorial_Id");
            AlterColumn("dbo.Comments", "CollaborativeTutorial_Id", c => c.Int());
            CreateIndex("dbo.Comments", "CollaborativeTutorial_Id");
            AddForeignKey("dbo.Comments", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials", "Id");
            DropColumn("dbo.AspNetUsers", "Feed_Id");
            DropColumn("dbo.Comments", "CollaborativeTutorialTitle");
            DropColumn("dbo.Comments", "Notifications_Id");
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "Notifications_Id", c => c.Int());
            AddColumn("dbo.Comments", "CollaborativeTutorialTitle", c => c.String());
            AddColumn("dbo.AspNetUsers", "Feed_Id", c => c.Int());
            DropForeignKey("dbo.Comments", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials");
            DropIndex("dbo.Comments", new[] { "CollaborativeTutorial_Id" });
            AlterColumn("dbo.Comments", "CollaborativeTutorial_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "CollaborativeTutorial_Id", newName: "CollaborativeTutorialId");
            CreateIndex("dbo.Comments", "Notifications_Id");
            CreateIndex("dbo.Comments", "CollaborativeTutorialId");
            CreateIndex("dbo.AspNetUsers", "Feed_Id");
            AddForeignKey("dbo.Comments", "CollaborativeTutorialId", "dbo.CollaborativeTutorials", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Feed_Id", "dbo.Notifications", "Id");
            AddForeignKey("dbo.Comments", "Notifications_Id", "dbo.Notifications", "Id");
        }
    }
}
