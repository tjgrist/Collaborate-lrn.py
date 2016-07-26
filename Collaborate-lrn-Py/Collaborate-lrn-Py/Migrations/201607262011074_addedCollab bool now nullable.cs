namespace Collaborate_lrn_Py.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCollabboolnownullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tutorials", "AddedCollaborator", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tutorials", "AddedCollaborator", c => c.Boolean(nullable: false));
        }
    }
}
