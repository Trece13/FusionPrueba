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
    public class Ent_tticol090
    {

        [DataMember(Order = 0)]
        [Column(Name = "FPDN")]
        public string fpdn	{get; set;}

        [DataMember(Order = 1)]
        [Column(Name = "TPDN")]
        public string tpdn { get; set; }         

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item	{get; set;}

        [DataMember(Order = 3)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "SRNO")]
        public int srno { get; set; } 

        [DataMember(Order = 5)]
        [Column(Name = "QANA")]
        public decimal qana	{get; set;} 

        [DataMember(Order = 6)]
        [Column(Name = "LOGN")]
        public string logn	{get; set;}

        [DataMember(Order = 7)]
        [Column(Name = "DATE")]
        public string date {get; set;}

        [DataMember(Order = 8)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        public Ent_tticol090()
        {

            fpdn = string.Empty;
            tpdn = string.Empty;
            clot = string.Empty;
            srno = 0;
            item = string.Empty;
            qana = 0;
            logn = string.Empty;
            date = string.Empty;
            proc = 0;
            refcntd = 0;
            refcntu = 0;
            idrecord = string.Empty;
        }

        public Ent_tticol090(string pfpdn, string ptpdn, string pclot, int psrno, string pitem, decimal pqana,
                             string plogn, string pdate, int pproc, int prefcntd, int prefcntu, string pidrecord)
        {
            this.fpdn = pfpdn; 
            this.tpdn = ptpdn; 
            this.clot = pclot; 
            this.srno = psrno; 
            this.item = pitem; 
            this.qana = pqana; 
            this.logn = plogn; 
            this.date = pdate; 
            this.proc = pproc; 
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.idrecord = pidrecord;
        }

        
    }
}
