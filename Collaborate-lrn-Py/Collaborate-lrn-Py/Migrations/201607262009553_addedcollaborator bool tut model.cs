namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcollaboratorbooltutmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "AddedCollaborator", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutorials", "AddedCollaborator");
        }
    }
}
