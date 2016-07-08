namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answerfieldtoquiz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "Answer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "Answer");
        }
    }
}
