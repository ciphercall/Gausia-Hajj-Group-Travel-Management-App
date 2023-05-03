using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Models.DTO
{
    public class RPDTO
    {
       
        public Int64 Id { get; set; }

        public Int64 CompanyId { get; set; }
      
        public string TransactionDate { get; set; }
      
        public string Transmonthyear { get; set; }
       
        public Int64 TransactionNo { get; set; }
        public string TransactionSerial { get; set; }
     
        public Int64 Cardcid { get; set; }
       
        public Int64 Cardpid { get; set; }

        public Int64 Cardyear { get; set; }
        public string Cardno { get; set; }
        public string passenger { get; set; }

        public string Cardid { get; set; }
      
        public string TransactionFor { get; set; }
      
        public Int64 Accountcd { get; set; }
        public string Accountnm { get; set; }
 
        public string Ticketno { get; set; }
    
        public string TicketDate{ get; set; }
     
        public decimal? Amount { get; set; }
       
        public decimal? Cashamount { get; set; }
      

        public decimal? Creditamount { get; set; }
        public string Remarks { get; set; }






     
        public string USERPC { get; set; }
        public Int64? INSUSERID { get; set; }

      
        public DateTime? INSTIME { get; set; }

     
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64? UPDUSERID { get; set; }

     
        public DateTime? UPDTIME { get; set; }

     
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}