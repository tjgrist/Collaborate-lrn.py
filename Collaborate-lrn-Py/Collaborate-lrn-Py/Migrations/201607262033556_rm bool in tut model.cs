namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmboolintutmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tutorials", "AddedCollaborator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorials", "AddedCollaborator", c => c.Boolean());
        }
    }
}
