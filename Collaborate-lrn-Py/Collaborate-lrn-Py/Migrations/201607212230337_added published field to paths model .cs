namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpublishedfieldtopathsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paths", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paths", "Published");
        }
    }
}
