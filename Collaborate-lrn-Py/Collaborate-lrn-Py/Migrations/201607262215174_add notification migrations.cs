namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotificationmigrations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Read");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Read", c => c.Boolean(nullable: false));
        }
    }
}
