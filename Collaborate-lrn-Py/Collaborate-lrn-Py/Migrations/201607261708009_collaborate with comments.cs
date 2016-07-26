namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collaboratewithcomments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Text = c.String(),
                        Votes = c.Int(nullable: false),
                        CollaborativeTutorial_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CollaborativeTutorials", t => t.CollaborativeTutorial_Id)
                .Index(t => t.CollaborativeTutorial_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CollaborativeTutorial_Id", "dbo.CollaborativeTutorials");
            DropIndex("dbo.Comments", new[] { "CollaborativeTutorial_Id" });
            DropTable("dbo.Comments");
        }
    }
}
