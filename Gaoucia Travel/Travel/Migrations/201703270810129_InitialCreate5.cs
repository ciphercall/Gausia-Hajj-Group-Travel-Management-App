namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GL_STRANS", "CARDNO", c => c.String());
            AddColumn("dbo.GL_STRANS", "CARDID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GL_STRANS", "CARDID");
            DropColumn("dbo.GL_STRANS", "CARDNO");
        }
    }
}
