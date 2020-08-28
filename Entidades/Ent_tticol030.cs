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
    public class Ent_tticol030
    {

        [DataMember(Order = 0)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "SQNB")]
        public int sqnb { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "MITM")]
        public string mitm { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }


        public Ent_tticol030()
        {
            pdno = string.Empty;
            sqnb = 0;
            mitm = string.Empty;
            dsca = string.Empty;
            cwar = string.Empty;
            loca = string.Empty;
            qtdl = 0;
            cuni = string.Empty;
            date = string.Empty;
            mess = string.Empty;
            user = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_tticol030(   string _pdno,   int _sqnb,      string _mitm,   string _dsca,   string _cwar,   string _loca,   decimal _qtdl,   
                                string _cuni,   string _date,   string _mess,   string _user,   int _refcntd,   int _refcntu)
        {
            this.pdno = _pdno;
            this.sqnb = _sqnb;
            this.mitm = _mitm;
            this.dsca = _dsca;
            this.cwar = _cwar;
            this.loca = _loca;
            this.qtdl = _qtdl;
            this.cuni = _cuni;
            this.date = _date;
            this.mess = _mess;
            this.user = _user;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;
        }     


    }
}


