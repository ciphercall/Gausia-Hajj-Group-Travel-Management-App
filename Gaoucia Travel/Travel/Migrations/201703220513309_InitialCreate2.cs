namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TAMS_PASSENGER", "CARDID", c => c.String());
            AlterColumn("dbo.TAMS_PASSENGER", "CARDCID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TAMS_PASSENGER", "CARDCID", c => c.Long());
            AlterColumn("dbo.TAMS_PASSENGER", "CARDID", c => c.Long());
        }
    }
}
