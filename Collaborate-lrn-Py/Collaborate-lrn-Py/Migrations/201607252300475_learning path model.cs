namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class learningpathmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LearningPathModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "LearningPathModel_Id", c => c.Int());
            AddColumn("dbo.Paths", "LearningPathModel_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "LearningPathModel_Id");
            CreateIndex("dbo.Paths", "LearningPathModel_Id");
            AddForeignKey("dbo.Paths", "LearningPathModel_Id", "dbo.LearningPathModels", "Id");
            AddForeignKey("dbo.AspNetUsers", "LearningPathModel_Id", "dbo.LearningPathModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LearningPathModel_Id", "dbo.LearningPathModels");
            DropForeignKey("dbo.Paths", "LearningPathModel_Id", "dbo.LearningPathModels");
            DropIndex("dbo.Paths", new[] { "LearningPathModel_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "LearningPathModel_Id" });
            DropColumn("dbo.Paths", "LearningPathModel_Id");
            DropColumn("dbo.AspNetUsers", "LearningPathModel_Id");
            DropTable("dbo.LearningPathModels");
        }
    }
}
