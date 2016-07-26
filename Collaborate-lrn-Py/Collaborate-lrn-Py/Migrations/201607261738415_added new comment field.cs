namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewcommentfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollaborativeTutorials", "newComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollaborativeTutorials", "newComment");
        }
    }
}
