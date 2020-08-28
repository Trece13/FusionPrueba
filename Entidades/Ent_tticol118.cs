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
    public class Ent_tticol118
    {
        [DataMember(Order = 0)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "QTYR")]
        public double qtyr { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "CDIS")]
        public string cdis { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "OBSE")]
        public string obse { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "LOGR")]
        public string logr { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DATR")]
        public string datr { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "DISP")]
        public int disp { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "STOC")]
        public string stoc { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "RITM")]
        public string ritm { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "SUNO")]
        public string suno { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 15)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

		[DataMember(Order = 16)]
        [Column(Name = "PAID")]
        public string paid { get; set; }
		
        public Ent_tticol118()
        {
            item = string.Empty;
            cwar = string.Empty;
            clot = string.Empty;
            qtyr = 0;
            cdis = string.Empty;
            obse = string.Empty;
            logr = string.Empty;
            datr = string.Empty;
            disp = 0;
            stoc = string.Empty;
            ritm = string.Empty;
            proc = 0;
            mess = string.Empty;
            suno = string.Empty;
            refcntd = 0;
            refcntu = 0;
            paid = string.Empty;
        }


        public Ent_tticol118(string pitem, string pcwar, string pclot, double pqtyr, string pcdis, string pobse, string plogr, string pdatr,
                            int pdisp, string pstoc, string pritm, int pproc, string pmess, string psuno, int prefcntd, int prefcntu, string ppaid)
        {
            this.item = pitem;
            this.cwar = pcwar;
            this.clot = pclot;
            this.qtyr = pqtyr;
            this.cdis = pcdis;
            this.obse = pobse;
            this.logr = plogr;
            this.datr = pdatr;
            this.disp = pdisp;
            this.stoc = pstoc;
            this.ritm = pritm;
            this.proc = pproc;
            this.mess = pmess;
            this.suno = psuno;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.paid = ppaid;
        }    
    }
}


 
 
