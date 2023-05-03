using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Travel.Models.Account;
using Travel.Models.ASL;
using Travel.Models.Travel;


namespace Travel.Models
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext()
            : base("name=TravelDbContext")
        {
            //base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<AslCompany> AslCompanyDbSet { get; set; }
        public DbSet<Asl_Branch> Asl_BranchDbSet { get; set; }
        public DbSet<AslUserco> AslUsercoDbSet { get; set; }
        public DbSet<ASL_LOG> AslLogDbSet { get; set; }
        public DbSet<ASL_DELETE> AslDeleteDbSet { get; set; }
        public DbSet<ASL_MENUMST> AslMenumstDbSet { get; set; }
        public DbSet<ASL_MENU> AslMenuDbSet { get; set; }
        public DbSet<ASL_ROLE> AslRoleDbSet { get; set; }


    
      



     

        //Acount
        public DbSet<GL_ACCHART> GlAcchartDbSet { get; set; }
        public DbSet<GL_COSTPMST> GLCostPMSTDbSet { get; set; }
        public DbSet<GL_COSTPOOL> GlCostPoolDbSet { get; set; }
        public DbSet<GL_STRANS> GlStransDbSet { get; set; }
        public DbSet<GL_MASTER> GlMasterDbSet { get; set; }
        public DbSet<GL_MTRANS> MtransdbSet { get; set; }
        public DbSet<GL_MTRANSMST> MtransMasterdbSet { get; set; }

        //Upload Excel File Data module
        public DbSet<ASL_PCONTACTS> UploadContactDbSet { get; set; }
        public DbSet<ASL_PGROUPS> UploadGroupDbSet { get; set; }
        public DbSet<ASL_PEMAIL> SendLogEmailDbSet { get; set; }
        public DbSet<ASL_PSMS> SendLogSMSDbSet { get; set; }


        //Travel
        public DbSet<TAMS_CARRIER> carrierDbSet { get; set; }
        public DbSet<TAMS_LICENSE> licenseDbSet { get; set; }
        public DbSet<TAMS_FLIGHT> flightDbSet { get; set; }
        public DbSet<TAMS_PASSENGER> passengerDbSet { get; set; }
        public DbSet<TAMS_PSNGRCARD> passengerCardDbSet { get; set; }
        public DbSet<TAMS_RPDRCR> RPDRCRDbSet { get; set; }
        public DbSet<TAMS_REFUND> refundDbSet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}