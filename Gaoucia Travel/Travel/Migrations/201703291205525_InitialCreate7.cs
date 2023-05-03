namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GL_MASTER", "CARDNO", c => c.String());
            AddColumn("dbo.GL_MASTER", "CARDID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GL_MASTER", "CARDID");
            DropColumn("dbo.GL_MASTER", "CARDNO");
        }
    }
}
