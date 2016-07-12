namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedfloattointforrating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tutorials", "Rating", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tutorials", "Rating", c => c.Single());
        }
    }
}
