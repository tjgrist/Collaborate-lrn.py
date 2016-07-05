namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtoonetoone : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Quizs", new[] { "Tutorial_ID" });
            RenameColumn(table: "dbo.Quizs", name: "Tutorial_ID", newName: "TutorialId");
            DropPrimaryKey("dbo.Quizs");
            AlterColumn("dbo.Quizs", "TutorialId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Quizs", "TutorialId");
            CreateIndex("dbo.Quizs", "TutorialId");
            DropColumn("dbo.Quizs", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quizs", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.Quizs", new[] { "TutorialId" });
            DropPrimaryKey("dbo.Quizs");
            AlterColumn("dbo.Quizs", "TutorialId", c => c.Int());
            AddPrimaryKey("dbo.Quizs", "Id");
            RenameColumn(table: "dbo.Quizs", name: "TutorialId", newName: "Tutorial_ID");
            CreateIndex("dbo.Quizs", "Tutorial_ID");
        }
    }
}
