namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcollabtuttocomments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CollaborativeTutorialTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CollaborativeTutorialTitle");
        }
    }
}
