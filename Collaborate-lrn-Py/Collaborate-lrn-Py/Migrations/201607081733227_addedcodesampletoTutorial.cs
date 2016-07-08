namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcodesampletoTutorial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "CodeSample", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutorials", "CodeSample");
        }
    }
}
