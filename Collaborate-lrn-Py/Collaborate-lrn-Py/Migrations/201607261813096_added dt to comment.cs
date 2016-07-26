namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddttocomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "TimeStamp");
        }
    }
}
