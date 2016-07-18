namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collabtutorialstousermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tutorials", "ApplicationUser_Id");
            AddForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tutorials", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tutorials", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Tutorials", "ApplicationUser_Id");
        }
    }
}
