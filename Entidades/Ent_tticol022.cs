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
    public class Ent_tticol022 : Ent_tticol222
    {
        [DataMember(Order = 0)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "SQNB")]
        public string sqnb { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "PROC")]
        public int proc { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "DATE")]
        public string date { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "MITM")]
        public string mitm { get; set; }


        [DataMember(Order = 6)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }


        [DataMember(Order = 7)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }


        [DataMember(Order = 8)]
        [Column(Name = "LOG1")]
        public string log1 { get; set; }


        [DataMember(Order = 9)]
        [Column(Name = "DATC")]
        public string datc { get; set; }


        [DataMember(Order = 10)]
        [Column(Name = "QTD1")]
        public int qtd1 { get; set; }


        [DataMember(Order = 11)]
        [Column(Name = "PRO1")]
        public int pro1 { get; set; }


        [DataMember(Order = 12)]
        [Column(Name = "LOG2")]
        public string log2 { get; set; }


        [DataMember(Order = 13)]
        [Column(Name = "DATU")]
        public string datu { get; set; }


        [DataMember(Order = 14)]
        [Column(Name = "QTD2")]
        public int qtd2 { get; set; }


        [DataMember(Order = 15)]
        [Column(Name = "PRO2")]
        public int pro2 { get; set; }


        [DataMember(Order = 16)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }


        [DataMember(Order = 17)]
        [Column(Name = "NORP")]
        public int norp { get; set; }


        [DataMember(Order = 18)]
        [Column(Name = "DLRP")]
        public string dlrp { get; set; }


        [DataMember(Order = 19)]
        [Column(Name = "DELE")]
        public int dele { get; set; }


        [DataMember(Order = 20)]
        [Column(Name = "LOGD")]
        public string logd { get; set; }


        [DataMember(Order = 21)]
        [Column(Name = "DATD")]
        public string datd { get; set; }


        [DataMember(Order = 22)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 23)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 24)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        [DataMember(Order = 25)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }
        [DataMember(Order = 26)]
        public bool Error { get; set; }
        [DataMember(Order = 27)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 28)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 29)]
        public string SuccessMsg { get; set; }

        public Ent_tticol022()
        {
            pdno = string.Empty;
            sqnb = string.Empty;
            proc = 0;
            logn = string.Empty;
            date = string.Empty;
            mitm = string.Empty;
            qtdl = 0;
            cuni = string.Empty;
            log1 = string.Empty;
            datc = string.Empty;
            qtd1 = 0;
            pro1 = 0;
            log2 = string.Empty;
            datu = string.Empty;
            qtd2 = 0;
            pro2 = 0;
            loca = string.Empty;
            norp = 0;
            dlrp = string.Empty;
            dele = 0;
            logd = string.Empty;
            datd = string.Empty;
            refcntd = 0;
            refcntu = 0;
            idrecord = string.Empty;
            Error = false;
        }
        public Ent_tticol022(string ppdno, string psqnb, int pproc, string plogn, string pdate, string pmitm, decimal pqtdl,
                             string pcuni, string plog1, string pdatc, int pqtd1, int ppro1, string plog2, string pdatu,
                             int pqtd2, int ppro2, string ploca, int pnorp, string pdlrp, int pdele, string plogd, string pdatd,
                             int prefcntd, int prefcntu, string pidrecord)
        {
            this.pdno = ppdno;
            this.sqnb = psqnb;
            this.proc = pproc;
            this.logn = plogn;
            this.date = pdate;
            this.mitm = pmitm;
            this.qtdl = pqtdl;
            this.cuni = pcuni;
            this.log1 = plog1;
            this.datc = pdatc;
            this.qtd1 = pqtd1;
            this.pro1 = ppro1;
            this.log2 = plog2;
            this.datu = pdatu;
            this.qtd2 = pqtd2;
            this.pro2 = ppro2;
            this.loca = ploca;
            this.norp = pnorp;
            this.dlrp = pdlrp;
            this.dele = pdele;
            this.logd = plogd;
            this.datd = pdatd;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.idrecord = pidrecord;
            this.Error = false;
        }

    }
}
