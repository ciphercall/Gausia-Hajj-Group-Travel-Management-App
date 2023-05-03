using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Travel.Models.ASL
{
    public class Asl_Branch
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }

        [Key, Column(Order = 1)]
        public Int64 COMPID { get; set; }

        [Key, Column(Order = 2)]
        public Int64 BRANCHID { get; set; }

        [Required(ErrorMessage = "Branch Name can not be empty!")]
        [Display(Name = "Branch Name")]
        public string BRANCHNM { get; set; }

        [Display(Name = "Address")]
        public string ADDRESS { get; set; }

        [Required(ErrorMessage = "Mobile Number field can not be empty!")]
        //[Remote("Check_PhoneNumber", "AslUserCO", ErrorMessage = "User Mobile number already exists!")]
        [Display(Name = "Contact No")]
        [RegularExpression(@"^(8{2})([0-9]{11})", ErrorMessage = "Insert a valid phone Number like 8801900000000")]
        public string CONTACTNO { get; set; }

        //[Remote("Check_UserEmail", "AslUserCO", ErrorMessage = "User Email address already exists!")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email Address")]
        public string EMAILID { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "STATUS can not be empty!")]
        public string STATUS { get; set; }


        [Display(Name = "User PC")]
        public string USERPC { get; set; }
        public Int64 INSUSERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64 UPDUSERID { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}