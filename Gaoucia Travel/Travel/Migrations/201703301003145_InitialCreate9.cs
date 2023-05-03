namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAMS_RPDRCR",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(nullable: false),
                        TRANSNO = c.Long(nullable: false),
                        CARDCID = c.Long(nullable: false),
                        CARDPID = c.Long(nullable: false),
                        CARDYY = c.Long(nullable: false),
                        CARDID = c.Long(nullable: false),
                        TRANSFOR = c.String(nullable: false),
                        ACCOUNTCD = c.Long(nullable: false),
                        TICKETNO = c.String(nullable: false),
                        TICKETDT = c.DateTime(nullable: false),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AMTCASH = c.Decimal(precision: 18, scale: 2),
                        AMTCREDIT = c.Decimal(precision: 18, scale: 2),
                        REMARKS = c.String(),
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
            DropTable("dbo.TAMS_RPDRCR");
        }
    }
}
