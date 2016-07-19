namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madevotesnonnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tutorials", "Votes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tutorials", "Votes", c => c.Int());
        }
    }
}
