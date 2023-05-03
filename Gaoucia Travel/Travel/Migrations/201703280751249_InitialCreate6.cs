namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GL_MTRANS", "CARDNO", c => c.String());
            AddColumn("dbo.GL_MTRANS", "CARDID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GL_MTRANS", "CARDID");
            DropColumn("dbo.GL_MTRANS", "CARDNO");
        }
    }
}
