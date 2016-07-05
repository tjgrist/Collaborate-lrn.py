namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bodytext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "BodyText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutorials", "BodyText");
        }
    }
}
