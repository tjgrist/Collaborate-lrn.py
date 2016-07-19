namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingvotesintut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "Votes", c => c.Int());
            DropColumn("dbo.Tutorials", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorials", "Rating", c => c.Int());
            DropColumn("dbo.Tutorials", "Votes");
        }
    }
}
