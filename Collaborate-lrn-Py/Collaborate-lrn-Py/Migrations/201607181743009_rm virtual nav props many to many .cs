namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmvirtualnavpropsmanytomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials");
            DropForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Tutorial_ID" });
            DropIndex("dbo.Tutorials", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Tutorial_ID");
            DropColumn("dbo.Tutorials", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorials", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Tutorial_ID", c => c.Int());
            CreateIndex("dbo.Tutorials", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Tutorial_ID");
            AddForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials", "ID");
        }
    }
}
