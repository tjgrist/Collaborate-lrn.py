namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcollaborativetutorialsdbset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollaborativeTutorials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TutorialId = c.Int(nullable: false),
                        CollaboratorId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tutorials", t => t.TutorialId, cascadeDelete: true)
                .Index(t => t.TutorialId);
            
            AddColumn("dbo.AspNetUsers", "CollaborativeTutorials_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CollaborativeTutorials_Id");
            AddForeignKey("dbo.AspNetUsers", "CollaborativeTutorials_Id", "dbo.CollaborativeTutorials", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollaborativeTutorials", "TutorialId", "dbo.Tutorials");
            DropForeignKey("dbo.AspNetUsers", "CollaborativeTutorials_Id", "dbo.CollaborativeTutorials");
            DropIndex("dbo.AspNetUsers", new[] { "CollaborativeTutorials_Id" });
            DropIndex("dbo.CollaborativeTutorials", new[] { "TutorialId" });
            DropColumn("dbo.AspNetUsers", "CollaborativeTutorials_Id");
            DropTable("dbo.CollaborativeTutorials");
        }
    }
}
