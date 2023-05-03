namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAMS_FLIGHT",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        CARDYY = c.Long(nullable: false),
                        CARDID = c.Long(nullable: false),
                        CARRIERSID = c.String(),
                        FLIGHTNO = c.String(),
                        FLIGHTSL = c.Long(nullable: false),
                        FLIGHTDT = c.DateTime(),
                        RETURNDT = c.DateTime(),
                        CLASS = c.String(),
                        ROUTE = c.String(),
                        TIMEDEP = c.String(),
                        TIMEARR = c.String(),
                        STATUS = c.String(),
                        PNRNO = c.String(),
                        CONFIRMBY = c.String(),
                        FLIGHTDTS = c.DateTime(),
                        TKTTLIMIT = c.DateTime(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TAMS_FLIGHT");
        }
    }
}
