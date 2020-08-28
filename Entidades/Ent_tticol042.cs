using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using whusa;

namespace whusa.Entidades
{
    [DataContract]    
    public class Ent_tticol042:Ent_tticol242
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
        [Column(Name = "PONO")]        
        public int pono { get; set; }

        [DataMember(Order = 7)]        
        [Column(Name = "QTDL")]        
        public decimal qtdl { get; set; }

        [DataMember(Order = 8)]        
        [Column(Name = "CUNI")]        
        public string cuni { get; set; }

        [DataMember(Order = 9)]         
        [Column(Name = "LOG1")]       
        public string log1 { get; set; }

        [DataMember(Order = 10)]        
        [Column(Name = "DATC")]        
        public string datc { get; set; }

        [DataMember(Order = 11)]        
        [Column(Name = "QTD1")]        
        public decimal qtd1 { get; set; }

        [DataMember(Order = 12)]         
        [Column(Name = "PRO1")]        
        public int pro1 { get; set; }

        [DataMember(Order = 13)]        
        [Column(Name = "LOG2")]        
        public string log2 { get; set; }

        [DataMember(Order = 14)]        
        [Column(Name = "DATU")]        
        public string datu { get; set; }

        [DataMember(Order = 15)]         
        [Column(Name = "QTD2")]       
        public decimal qtd2 { get; set; }

        [DataMember(Order = 16)]         
        [Column(Name = "PRO2")]        
        public int pro2 { get; set; }

        [DataMember(Order = 17)]         
        [Column(Name = "LOCA")]        
        public string loca { get; set; }

        [DataMember(Order = 18)]         
        [Column(Name = "NORP")]        
        public int norp { get; set; }

        [DataMember(Order = 19)]         
        [Column(Name = "DLRP")]        
        public string dlrp { get; set; }

        [DataMember(Order = 20)]        
        [Column(Name = "DELE")]        
        public int dele { get; set; }

        [DataMember(Order = 21)]         
        [Column(Name = "LOGD")]        
        public string logd { get; set; }

        [DataMember(Order = 22)]         
        [Column(Name = "DATD")]
        public string datd { get; set; }

        [DataMember(Order = 23)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 24)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }
        
        [DataMember(Order = 25)]
        public bool Error { get; set; }

        [DataMember(Order = 26)]
        public string TypeMsgJs { get; set; }

        [DataMember(Order = 27)]
        public string ErrorMsg { get; set; }


        [DataMember(Order = 28)]
        public string dsca { get; set; }

        [DataMember(Order = 29)]
        public string kltc { get; set; }

        [DataMember(Order = 30)]
        public string wreg { get; set; }

        public Ent_tticol042()
        {
            pdno = string.Empty;
            sqnb = string.Empty;
            proc = 0;
            logn = string.Empty;
            date = string.Empty;
            mitm = string.Empty;
            pono = 0;
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
            wreg = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_tticol042(string _pdno,  string _sqnb,  int _proc,     string _logn,  string _date,  string _mitm,  int _pono,     decimal _qtdl,
                             string _cuni,  string _log1,  string _datc,  decimal _qtd1, int _pro1,     string _log2,  string _datu,  decimal _qtd2,  
                             int _pro2,     string _loca,  int _norp,     string _dlrp,  int _dele,     string _logd,  string _datd,  int _refcntd,
                             int _refcntu, string _wreg)
        {
            this.pdno = _pdno;
            this.sqnb = _sqnb;
            this.proc = _proc;
            this.logn = _logn;
            this.date = _date;
            this.mitm = _mitm;
            this.pono = _pono;
            this.qtdl = _qtdl;
            this.cuni = _cuni;
            this.log1 = _log1;
            this.datc = _datc;
            this.qtd1 = _qtd1;
            this.pro1 = _pro1;
            this.log2 = _log2;
            this.datu = _datu;
            this.qtd2 = _qtd2;
            this.pro2 = _pro2;
            this.loca = _loca;
            this.norp = _norp;
            this.dlrp = _dlrp;
            this.dele = _dele;
            this.logd = _logd;
            this.datd = _datd;
            this.wreg = wreg;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;
        }

    }
}
