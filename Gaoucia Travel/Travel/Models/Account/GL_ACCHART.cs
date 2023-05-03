using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Travel.Models.Account
{
    [Table("GL_ACCHART")]
    public class GL_ACCHART
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }

        [Key, Column(Order = 1)]
        public Int64 COMPID { get; set; }

        [Key, Column(Order = 2)]
        public Int64 HEADTP { get; set; }    //--1:ASSET, 2:LIABILITY.....
        public Int64 CONTROLCD { get; set; } //--101101000000000

        [Key, Column(Order = 3)]
        public Int64 ACCOUNTCD { get; set; } //--101101010000000
        public string ACCOUNTNM { get; set; }
        public Int64 BRANCHID { get; set; }
        public Int64 HLEVELCD { get; set; }

        [StringLength(1, MinimumLength = 1)]
        public string HDRCRTP { get; set; }
     
        public string HPOSTTP { get; set; }
        [StringLength(1, MinimumLength = 1)]
        public string HSTATUS { get; set; }




        public string USERPC { get; set; }
        public Int64 INSUSERID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64 UPDUSERID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}