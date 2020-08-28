using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace whusa.Entidades
{
    [DataContract]
    public class Ent_ttwhcol017
    {

        [DataMember(Order = 0)]
        [Column(Name = "LABL")]   
        public string labl { get; set; }   

        [DataMember(Order = 1)]
        [Column(Name = "SQNB")]
        public int sqnb { get; set; }           
        
        [DataMember(Order = 2)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }   
        
        [DataMember(Order = 3)]
        [Column(Name = "ITEM")]
        public string item { get; set; }   

        [DataMember(Order = 4)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }   
        
        [DataMember(Order = 5)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }   
        
        [DataMember(Order = 6)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }   
        
        [DataMember(Order = 7)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }   
        
        [DataMember(Order = 8)]
        [Column(Name = "DATE")]
        public string date { get; set; }   
        
        [DataMember(Order = 9)]
        [Column(Name = "COUN")]
        public int coun { get; set; }   
        
        [DataMember(Order = 10)]
        [Column(Name = "PROC")]
        public int proc { get; set; }   
        
        [DataMember(Order = 11)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }
        
        [DataMember(Order = 12)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "ZONE")]
        public string zone { get; set; }

        public Ent_ttwhcol017()
        {
            labl = " ";
            sqnb = 0;
            cwar = " ";
            item = string.Empty;
            clot = " ";
            qtdl = 0;   
            cuni = string.Empty;
            logn = string.Empty;
            date = string.Empty;
            coun = 0;   
            proc = 0;  
            refcntd = 0;
            refcntu = 0;
            zone = " ";
        }

        public Ent_ttwhcol017(string plabl, int psqnb, string pcwar, string pitem,
                              string pclot, decimal pqtdl, string pcuni, string plogn,
                              string pdate, int pcoun, int pproc, int prefcntd, 
                              int prefcntu, string zone)
        {
            this.labl = plabl;
            this.sqnb = psqnb;
            this.cwar = pcwar;
            this.item = pitem;
            this.clot = pclot;
            this.qtdl = pqtdl;
            this.cuni = pcuni;
            this.logn = plogn;
            this.date = pdate;
            this.coun = pcoun;
            this.proc = pproc;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.zone = zone;

        }
    }
}














