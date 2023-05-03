namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAMS_REFUND",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        TRANSDT = c.DateTime(),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(nullable: false),
                        ACCOUNTCD = c.Long(nullable: false),
                        CARDCID = c.Long(nullable: false),
                        CARDPID = c.Long(nullable: false),
                        CARDYY = c.Long(nullable: false),
                        CARDID = c.String(),
                        TICKETNO = c.String(),
                        TICKETDT = c.DateTime(),
                        BUYAMTS = c.Decimal(precision: 18, scale: 2),
                        REFUNDS = c.Decimal(precision: 18, scale: 2),
                        DEDAMTS = c.Decimal(precision: 18, scale: 2),
                        SALAMTT = c.Decimal(precision: 18, scale: 2),
                        REFUNDT = c.Decimal(precision: 18, scale: 2),
                        DEDAMTT = c.Decimal(precision: 18, scale: 2),
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
            DropTable("dbo.TAMS_REFUND");
        }
    }
}
