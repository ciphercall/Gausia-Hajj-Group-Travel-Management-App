namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAMS_PASSENGER",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        CARDDT = c.DateTime(),
                        CARDYY = c.Long(),
                        CARDNO = c.String(),
                        CARDID = c.Long(),
                        CARDCID = c.Long(),
                        PKGSTP = c.String(),
                        AGENTID = c.Long(nullable: false),
                        LICENSEID = c.Long(nullable: false),
                        PSGRNM = c.String(nullable: false),
                        FATHERNM = c.String(),
                        MOTHERNM = c.String(),
                        PSGRTP = c.String(),
                        DOB = c.DateTime(),
                        NATIONALITY = c.String(),
                        MSTATUS = c.String(),
                        GENDER = c.String(),
                        BLOODG = c.String(),
                        EDUCATION = c.String(),
                        PROFESSION = c.String(),
                        DESIGNATION = c.String(),
                        BIRTHPLACE = c.String(),
                        COUNTRY = c.String(),
                        NID = c.String(),
                        PASSPORTNO = c.String(),
                        ISSUEDT = c.DateTime(),
                        ISSUEPLACE = c.String(),
                        EXPIREDT = c.DateTime(),
                        PASSPORTTP = c.String(),
                        PERADDR = c.String(),
                        PERPO = c.String(),
                        PERPC = c.String(),
                        PERPS = c.String(),
                        PERDIST = c.String(),
                        PREADDR = c.String(),
                        PREPO = c.String(),
                        PREPC = c.String(),
                        PREPS = c.String(),
                        PREDIST = c.String(),
                        TELNO = c.String(),
                        MOBNO = c.String(),
                        GRDNM = c.String(),
                        GRDREL = c.String(),
                        GRDMOBNO = c.String(),
                        GRDEMAIL = c.String(),
                        GRDWPSGR = c.String(),
                        MOALLEMNM = c.String(),
                        MOALLEMADDR = c.String(),
                        MOALLEMMOBNO = c.String(),
                        PKGSAMT = c.Decimal(precision: 18, scale: 2),
                        MOALLEMFEE = c.Decimal(precision: 18, scale: 2),
                        CARDIDPIC = c.String(),
                        CARRIERID = c.Long(),
                        ROUTE = c.String(),
                        VNTP = c.String(),
                        VNNO = c.String(),
                        VNIDT = c.DateTime(),
                        VNEDT = c.DateTime(),
                        VNIPLACE = c.String(),
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
            DropTable("dbo.TAMS_PASSENGER");
        }
    }
}
