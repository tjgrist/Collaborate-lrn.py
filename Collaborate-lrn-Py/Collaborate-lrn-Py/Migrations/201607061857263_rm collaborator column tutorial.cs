namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmcollaboratorcolumntutorial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials");
            DropIndex("dbo.AspNetUsers", new[] { "Tutorial_ID" });
            DropColumn("dbo.AspNetUsers", "Tutorial_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Tutorial_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Tutorial_ID");
            AddForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials", "ID");
        }
    }
}
