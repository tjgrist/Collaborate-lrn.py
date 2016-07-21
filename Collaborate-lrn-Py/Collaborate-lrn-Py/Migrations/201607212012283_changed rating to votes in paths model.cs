namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedratingtovotesinpathsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paths", "Votes", c => c.Int(nullable: false));
            DropColumn("dbo.Paths", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paths", "Rating", c => c.Single(nullable: false));
            DropColumn("dbo.Paths", "Votes");
        }
    }
}
