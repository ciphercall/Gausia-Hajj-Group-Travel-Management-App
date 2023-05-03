namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GL_COSTPMST", "COSTICD", c => c.Long(nullable: false));
            AddColumn("dbo.GL_COSTPMST", "COSTECD", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GL_COSTPMST", "COSTECD");
            DropColumn("dbo.GL_COSTPMST", "COSTICD");
        }
    }
}
