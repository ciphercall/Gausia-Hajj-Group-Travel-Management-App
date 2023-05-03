using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel.Models.Travel
{
    public class TAMS_PSNGRCARD
    {

        [Key]
        public Int64 Id { get; set; }
        public Int64? COMPID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CARDDT { get; set; }
        public Int64? CARDYY { get; set; }
        public string CARDNO { get; set; }
        public string CARDNOP { get; set; }

        public string CARDID { get; set; }

        public string CARDCID { get; set; }
        public string PKGSTP { get; set; }
        [Required(ErrorMessage = "Agent Name required!")]
        public Int64 AGENTID { get; set; }

        public Int64 LICENSEID { get; set; }
        [Required(ErrorMessage = "Passenger Name required!")]
        public string PSGRNM { get; set; }
        public string FATHERNM { get; set; }
        public string MOTHERNM { get; set; }
        public string PSGRTP { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        public string NATIONALITY { get; set; }
        public string MSTATUS { get; set; }
        public string GENDER { get; set; }
        public string BLOODG { get; set; }

        public string EDUCATION { get; set; }
        public string PROFESSION { get; set; }
        public string DESIGNATION { get; set; }
        public string BIRTHPLACE { get; set; }
        public string COUNTRY { get; set; }
        public string NID { get; set; }
        public string PASSPORTNO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ISSUEDT { get; set; }
        public string ISSUEPLACE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EXPIREDT { get; set; }
        public string PASSPORTTP { get; set; }
        public string PERADDR { get; set; }
        public string PERPO { get; set; }
        public string PERPC { get; set; }
        public string PERPS { get; set; }
        public string PERDIST { get; set; }
        public string PREADDR { get; set; }
        public string PREPO { get; set; }
        public string PREPC { get; set; }
        public string PREPS { get; set; }
        public string PREDIST { get; set; }
        public string TELNO { get; set; }
        public string MOBNO { get; set; }
        public string GRDNM { get; set; }
        public string GRDREL { get; set; }
        public string GRDMOBNO { get; set; }
        public string GRDEMAIL { get; set; }

        public string GRDWPSGR { get; set; }
        public string MOALLEMNM { get; set; }
        public string MOALLEMADDR { get; set; }
        public string MOALLEMMOBNO { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal? PKGSAMT { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal? MOALLEMFEE { get; set; }
        public string CARDIDPIC { get; set; }
        public Int64? CARRIERID { get; set; }
        public string ROUTE { get; set; }

        public string VNTP { get; set; }
        public string VNNO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNIDT { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNEDT { get; set; }
        public string VNIPLACE { get; set; }
        public string REMARKS { get; set; }

        public Int64 NGNO { get; set; }
        public Int64 PILGRIMID { get; set; }


        [Display(Name = "User PC")]
        public string USERPC { get; set; }
        public Int64? INSUSERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64? UPDUSERID { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }

    }
}