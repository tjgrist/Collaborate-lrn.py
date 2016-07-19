namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanyrltocollabtutorials : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CollaborativeTutorials_Id", "dbo.CollaborativeTutorials");
            DropIndex("dbo.AspNetUsers", new[] { "CollaborativeTutorials_Id" });
            CreateTable(
                "dbo.ApplicationUserCollaborativeTutorials",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        CollaborativeTutorial_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.CollaborativeTutorial_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.CollaborativeTutorials", t => t.CollaborativeTutorial_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.CollaborativeTutorial_Id);
            
            DropColumn("dbo.CollaborativeTutorials", "CollaboratorId");
            DropColumn("dbo.AspNetUsers", "CollaborativeTutorials_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CollaborativeTutorials_Id", c => c.Int());
            AddColumn("dbo.CollaborativeTutorials", "CollaboratorId", c => c.String());
            DropForeignKey("dbo.ApplicationUserCollaborativeTutorials", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials");
            DropForeignKey("dbo.ApplicationUserCollaborativeTutorials", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCollaborativeTutorials", new[] { "CollaborativeTutorial_Id" });
            DropIndex("dbo.ApplicationUserCollaborativeTutorials", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserCollaborativeTutorials");
            CreateIndex("dbo.AspNetUsers", "CollaborativeTutorials_Id");
            AddForeignKey("dbo.AspNetUsers", "CollaborativeTutorials_Id", "dbo.CollaborativeTutorials", "Id");
        }
    }
}
