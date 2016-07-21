namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedexpectedinputfieldtoquizmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "ExpectedInput", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "ExpectedInput");
        }
    }
}
