namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmtutorialicollectionforuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tutorials", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Tutorials", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorials", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tutorials", "ApplicationUser_Id");
            AddForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
