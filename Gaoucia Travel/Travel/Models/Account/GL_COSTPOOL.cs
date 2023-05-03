﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel.Models
{
    public class GL_COSTPOOL
    {
        [Key]
        public Int64 CostPLID { get; set; }

        [Display(Name = "Company ID")]
        public Int64? COMPID { get; set; }



        [Display(Name = "COST ID")]
        public Int64? COSTCID { get; set; }

        [Display(Name = "Cost PID")]
        public Int64? COSTPID { get; set; }

        [Required(ErrorMessage = "Cost P Name Cannot be empty!")]
        [Display(Name = "Cost P Name")]
        public string COSTPNM { get; set; }

        [Display(Name = "Cost PSID")]
        public string COSTPSID { get; set; }
        [Display(Name = "Remarks")]
        public string REMARKS { get; set; }







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