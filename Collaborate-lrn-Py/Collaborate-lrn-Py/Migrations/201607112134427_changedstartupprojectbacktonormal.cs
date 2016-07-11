namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedstartupprojectbacktonormal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Tutorial_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Tutorial_ID");
            AddForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Tutorial_ID", "dbo.Tutorials");
            DropIndex("dbo.AspNetUsers", new[] { "Tutorial_ID" });
            DropColumn("dbo.AspNetUsers", "Tutorial_ID");
        }
    }
}
