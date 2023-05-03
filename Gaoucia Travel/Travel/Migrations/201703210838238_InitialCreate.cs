namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asl_Branch",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        BRANCHID = c.Long(nullable: false),
                        BRANCHNM = c.String(nullable: false),
                        ADDRESS = c.String(),
                        CONTACTNO = c.String(nullable: false),
                        EMAILID = c.String(),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.BRANCHID });
            
            CreateTable(
                "dbo.AslCompanies",
                c => new
                    {
                        AslCompanyId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COMPNM = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        CONTACTNO = c.String(nullable: false),
                        EMAILID = c.String(nullable: false),
                        WEBID = c.String(),
                        TRNOGTP = c.String(),
                        STATUS = c.String(nullable: false),
                        EMAILIDP = c.String(),
                        EMAILPWP = c.String(),
                        SMSIDP = c.String(),
                        SMSPWP = c.String(),
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
                .PrimaryKey(t => t.AslCompanyId);
            
            CreateTable(
                "dbo.ASL_DELETE",
                c => new
                    {
                        Asl_DeleteID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        DELSLNO = c.Long(),
                        DELDATE = c.String(),
                        DELTIME = c.String(),
                        DELIPNO = c.String(),
                        DELLTUDE = c.String(),
                        TABLEID = c.String(),
                        DELDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_DeleteID);
            
            CreateTable(
                "dbo.ASL_LOG",
                c => new
                    {
                        Asl_LOGid = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        BRANCHID = c.Long(),
                        USERID = c.Long(),
                        LOGTYPE = c.String(),
                        LOGSLNO = c.Long(),
                        LOGDATE = c.DateTime(),
                        LOGTIME = c.String(),
                        LOGIPNO = c.String(),
                        LOGLTUDE = c.String(),
                        TABLEID = c.String(),
                        LOGDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_LOGid);
            
            CreateTable(
                "dbo.ASL_MENU",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MODULEID = c.String(),
                        SERIAL = c.Long(),
                        MENUTP = c.String(),
                        MENUID = c.String(),
                        MENUNM = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ASL_MENUMST",
                c => new
                    {
                        MODULEID = c.String(nullable: false, maxLength: 128),
                        MODULENM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.MODULEID);
            
            CreateTable(
                "dbo.ASL_ROLE",
                c => new
                    {
                        ASL_ROLEId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        USERID = c.Long(nullable: false),
                        MODULEID = c.String(nullable: false),
                        SERIAL = c.Long(),
                        MENUTP = c.String(nullable: false),
                        MENUID = c.String(),
                        STATUS = c.String(),
                        INSERTR = c.String(),
                        UPDATER = c.String(),
                        DELETER = c.String(),
                        MENUNAME = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.ASL_ROLEId);
            
            CreateTable(
                "dbo.AslUsercoes",
                c => new
                    {
                        AslUsercoId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        BRANCHID = c.Long(),
                        USERID = c.Long(),
                        USERNM = c.String(nullable: false),
                        DEPTNM = c.String(),
                        OPTP = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        MOBNO = c.String(nullable: false),
                        EMAILID = c.String(),
                        LOGINBY = c.String(nullable: false),
                        LOGINID = c.String(nullable: false),
                        LOGINPW = c.String(nullable: false),
                        TIMEFR = c.String(nullable: false),
                        TIMETO = c.String(nullable: false),
                        SHOWFRM = c.String(),
                        SHOWREP = c.String(),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.AslUsercoId);
            
            CreateTable(
                "dbo.TAMS_CARRIER",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        CARRIERID = c.Long(),
                        CARRIERSID = c.String(nullable: false, maxLength: 2),
                        CARRIERNO = c.String(nullable: false, maxLength: 3),
                        CARRIERNM = c.String(),
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
            
            CreateTable(
                "dbo.GL_ACCHART",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        HEADTP = c.Long(nullable: false),
                        ACCOUNTCD = c.Long(nullable: false),
                        CONTROLCD = c.Long(nullable: false),
                        ACCOUNTNM = c.String(),
                        BRANCHID = c.Long(nullable: false),
                        HLEVELCD = c.Long(nullable: false),
                        HDRCRTP = c.String(maxLength: 1),
                        HPOSTTP = c.String(),
                        HSTATUS = c.String(maxLength: 1),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.HEADTP, t.ACCOUNTCD });
            
            CreateTable(
                "dbo.GL_COSTPMST",
                c => new
                    {
                        CostMstID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTCNM = c.String(nullable: false),
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
                .PrimaryKey(t => t.CostMstID);
            
            CreateTable(
                "dbo.GL_COSTPOOL",
                c => new
                    {
                        CostPLID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTPID = c.Long(),
                        COSTPNM = c.String(nullable: false),
                        COSTPSID = c.String(),
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
                .PrimaryKey(t => t.CostPLID);
            
            CreateTable(
                "dbo.GL_MASTER",
                c => new
                    {
                        GL_MasterID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        BRANCHID = c.Long(nullable: false),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSSL = c.Long(),
                        TRANSDRCR = c.String(),
                        TRANSFOR = c.String(),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        DEBITAMT = c.Decimal(precision: 18, scale: 2),
                        CREDITAMT = c.Decimal(precision: 18, scale: 2),
                        TABLEID = c.String(),
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
                .PrimaryKey(t => t.GL_MasterID);
            
            CreateTable(
                "dbo.GL_STRANS",
                c => new
                    {
                        Gl_StransID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        BRANCHID = c.Long(nullable: false),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(nullable: false),
                        TRANSFOR = c.String(nullable: false),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.Gl_StransID);
            
            CreateTable(
                "dbo.TAMS_LICENSE",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        LICENSEID = c.Long(),
                        LICENSENO = c.String(),
                        LICENSENM = c.String(),
                        Remarks = c.String(),
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
            
            CreateTable(
                "dbo.GL_MTRANS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        BRANCHID = c.Long(nullable: false),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSSL = c.Long(),
                        TRANSFOR = c.String(),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GL_MTRANSMST",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        BRANCHID = c.Long(nullable: false),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ASL_PEMAIL",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        TRANSYY = c.Long(nullable: false),
                        TRANSNO = c.Long(nullable: false),
                        TRANSDT = c.DateTime(),
                        EMAILID = c.String(maxLength: 100),
                        EMAILSUBJECT = c.String(),
                        BODYMSG = c.String(),
                        STATUS = c.String(maxLength: 7),
                        SENTTM = c.DateTime(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.TRANSYY, t.TRANSNO });
            
            CreateTable(
                "dbo.ASL_PSMS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        TRANSYY = c.Long(nullable: false),
                        TRANSNO = c.Long(nullable: false),
                        TRANSDT = c.DateTime(),
                        MOBNO = c.String(maxLength: 13),
                        LANGUAGE = c.String(maxLength: 3),
                        MESSAGE = c.String(maxLength: 160),
                        STATUS = c.String(maxLength: 7),
                        SENTTM = c.DateTime(),
                        MSGTP = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.TRANSYY, t.TRANSNO });
            
            CreateTable(
                "dbo.ASL_PCONTACTS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        GROUPID = c.Long(nullable: false),
                        NAME = c.String(),
                        EMAIL = c.String(),
                        MOBNO1 = c.String(),
                        MOBNO2 = c.String(),
                        ADDRESS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID });
            
            CreateTable(
                "dbo.ASL_PGROUPS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        GROUPID = c.Long(nullable: false),
                        GROUPNM = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.COMPID, t.GROUPID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ASL_PGROUPS");
            DropTable("dbo.ASL_PCONTACTS");
            DropTable("dbo.ASL_PSMS");
            DropTable("dbo.ASL_PEMAIL");
            DropTable("dbo.GL_MTRANSMST");
            DropTable("dbo.GL_MTRANS");
            DropTable("dbo.TAMS_LICENSE");
            DropTable("dbo.GL_STRANS");
            DropTable("dbo.GL_MASTER");
            DropTable("dbo.GL_COSTPOOL");
            DropTable("dbo.GL_COSTPMST");
            DropTable("dbo.GL_ACCHART");
            DropTable("dbo.TAMS_CARRIER");
            DropTable("dbo.AslUsercoes");
            DropTable("dbo.ASL_ROLE");
            DropTable("dbo.ASL_MENUMST");
            DropTable("dbo.ASL_MENU");
            DropTable("dbo.ASL_LOG");
            DropTable("dbo.ASL_DELETE");
            DropTable("dbo.AslCompanies");
            DropTable("dbo.Asl_Branch");
        }
    }
}
