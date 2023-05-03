using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel.Models.Travel
{
    public class TAMS_RPDRCR
    {

        [Key]
        public Int64 Id { get; set; }

        public Int64 COMPID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Transaction Date can not be empty!")]
        public DateTime? TRANSDT { get; set; }
          [Required(ErrorMessage = "This is empty!")]
        public string TRANSMY { get; set; }
         [Required(ErrorMessage = "Transno can not be empty!")]
        public Int64 TRANSNO { get; set; }
         public Int64 TRANSSL { get; set; }
          [Required(ErrorMessage = "This is empty!")]
        public Int64 CARDCID { get; set; }
          [Required(ErrorMessage = "This is empty!")]
        public Int64 CARDPID { get; set; }
         
        public Int64 CARDYY { get; set; }
       
        public Int64 CARDID { get; set; }
          [Required(ErrorMessage = "TransFor can not be empty!")]
        public string TRANSFOR { get; set; }
          [Required(ErrorMessage = "AccountName can not be empty!")]
        public Int64 ACCOUNTCD { get; set; }
          [Required(ErrorMessage = "TicketNo can not be empty!")]
        public string TICKETNO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "TicketDate can not be empty!")]
        public DateTime? TICKETDT { get; set; }
          [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
          [Required(ErrorMessage = "Amount can not be empty!")]
        public decimal? AMOUNT { get; set; }
          [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal? AMTCASH { get; set; }
          [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
      
        public decimal? AMTCREDIT { get; set; }
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