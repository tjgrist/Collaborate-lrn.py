namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtwofieldstousermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CompletedTutorialsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CompletedTutorialsCount");
        }
    }
}
