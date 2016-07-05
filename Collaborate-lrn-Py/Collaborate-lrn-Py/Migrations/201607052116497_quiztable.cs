namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quiztable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Instruction = c.String(nullable: false),
                        EducatorId = c.String(maxLength: 128),
                        TutorialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EducatorId)
                .ForeignKey("dbo.Tutorials", t => t.TutorialId, cascadeDelete: true)
                .Index(t => t.EducatorId)
                .Index(t => t.TutorialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "TutorialId", "dbo.Tutorials");
            DropForeignKey("dbo.Quizs", "EducatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Quizs", new[] { "TutorialId" });
            DropIndex("dbo.Quizs", new[] { "EducatorId" });
            DropTable("dbo.Quizs");
        }
    }
}
