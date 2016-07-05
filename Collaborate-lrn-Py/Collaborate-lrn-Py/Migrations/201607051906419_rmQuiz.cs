namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmQuiz : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizs", "EducatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quizs", "Tutorial_ID", "dbo.Tutorials");
            DropIndex("dbo.Quizs", new[] { "EducatorId" });
            DropIndex("dbo.Quizs", new[] { "Tutorial_ID" });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Instruction = c.String(),
                        EducatorId = c.String(maxLength: 128),
                        Tutorial_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Quizs", "Tutorial_ID");
            CreateIndex("dbo.Quizs", "EducatorId");
            AddForeignKey("dbo.Quizs", "Tutorial_ID", "dbo.Tutorials", "ID");
            AddForeignKey("dbo.Quizs", "EducatorId", "dbo.AspNetUsers", "Id");
        }
    }
}
