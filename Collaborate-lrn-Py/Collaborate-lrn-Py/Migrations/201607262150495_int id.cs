namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials");
            DropIndex("dbo.Comments", new[] { "CollaborativeTutorial_Id" });
            RenameColumn(table: "dbo.Comments", name: "CollaborativeTutorial_Id", newName: "CollaborativeTutorialId");
            AlterColumn("dbo.Comments", "CollaborativeTutorialId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "CollaborativeTutorialId");
            AddForeignKey("dbo.Comments", "CollaborativeTutorialId", "dbo.CollaborativeTutorials", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CollaborativeTutorialId", "dbo.CollaborativeTutorials");
            DropIndex("dbo.Comments", new[] { "CollaborativeTutorialId" });
            AlterColumn("dbo.Comments", "CollaborativeTutorialId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "CollaborativeTutorialId", newName: "CollaborativeTutorial_Id");
            CreateIndex("dbo.Comments", "CollaborativeTutorial_Id");
            AddForeignKey("dbo.Comments", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials", "Id");
        }
    }
}
