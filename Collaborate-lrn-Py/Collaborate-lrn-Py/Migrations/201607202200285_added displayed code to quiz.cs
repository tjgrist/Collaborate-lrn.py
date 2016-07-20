namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddisplayedcodetoquiz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "DisplayedCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "DisplayedCode");
        }
    }
}
