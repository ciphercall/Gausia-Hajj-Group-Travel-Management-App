namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAMS_RPDRCR", "TRANSSL", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAMS_RPDRCR", "TRANSSL");
        }
    }
}
