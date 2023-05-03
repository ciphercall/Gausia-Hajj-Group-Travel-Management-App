using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Travel.Models.Account;
using Travel.Models.Travel;

namespace Travel.Models
{
    public class PageModel
    {

        public PageModel()
        {
            this.aslMenumst = new ASL_MENUMST();
            this.aslMenu = new ASL_MENU();
            this.aslUserco = new AslUserco();
            this.aslCompany = new AslCompany();
            this.aslLog = new ASL_LOG();




            this.AGlCostPMST = new GL_COSTPMST();
            this.AGlCostPool = new GL_COSTPOOL();

            this.AGlStrans = new GL_STRANS();
            this.AGlMaster = new GL_MASTER();
            this.AGL_acchart = new GL_ACCHART();


            this.carrier = new TAMS_CARRIER();
            this.license = new TAMS_LICENSE();
            this.flight = new TAMS_FLIGHT();
            this.passenger = new TAMS_PASSENGER();
            this.RPDRCR = new TAMS_RPDRCR();
            this.refund = new TAMS_REFUND();
                    
        }

        public ASL_MENUMST aslMenumst { get; set; }
        public ASL_MENU aslMenu { get; set; }
        public AslUserco aslUserco { get; set; }
        public AslCompany aslCompany { get; set; }
        public ASL_LOG aslLog { get; set; }

     


        public GL_COSTPMST AGlCostPMST { get; set; }
        public GL_COSTPOOL AGlCostPool { get; set; }


        public GL_STRANS AGlStrans { get; set; }
        public GL_MASTER AGlMaster { get; set; }
        public GL_ACCHART AGL_acchart { get; set; }



        public TAMS_CARRIER carrier { get; set; }
        public TAMS_LICENSE license { get; set; }
        public TAMS_FLIGHT flight { get; set; }
        public TAMS_PASSENGER passenger { get; set; }
        public TAMS_RPDRCR RPDRCR { get; set; }
        public TAMS_REFUND refund { get; set; }
    

        [Display(Name = "HeadType")]
        public string HeadType { get; set; }
        
        [Required(ErrorMessage = "From date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessage = "To Date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }



        //HMS-ReportController
        [Required(ErrorMessage = "From date field can not be empty !")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Report_FromDate { get; set; }

        [Required(ErrorMessage = "To Date field can not be empty !")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Report_ToDate { get; set; }

        [Required(ErrorMessage = "Department Name field can not be empty !")]
        public Int64 DEPTID { get; set; }

        [Required(ErrorMessage = "Transaction Type field can not be empty !")]
        public string TRANSTP { get; set; }

        [Required(ErrorMessage = "Input valid patient id !")]
        public string PATIENTYYID { get; set; }

        [Required(ErrorMessage = "Patient name field required !")]
        public string PATIENTNM { get; set; }

        [Required(ErrorMessage = "Store name field required !")]
        public string Store { get; set; }
        
        public string Item { get; set; }


        //CardInfoReport
        public string CardNoFrom { get; set; }
        public string CardNoTo { get; set; }

    }
}