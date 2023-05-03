namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAMS_PASSENGER", "NGNO", c => c.Long(nullable: false));
            AddColumn("dbo.TAMS_PASSENGER", "PILGRIMID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAMS_PASSENGER", "PILGRIMID");
            DropColumn("dbo.TAMS_PASSENGER", "NGNO");
        }
    }
}
