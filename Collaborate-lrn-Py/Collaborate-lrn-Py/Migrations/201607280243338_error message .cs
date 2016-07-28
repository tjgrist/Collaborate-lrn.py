namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errormessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "ErrorMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "ErrorMessage");
        }
    }
}
