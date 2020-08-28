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
    public class Ent_tticol020
    {
        [DataMember(Order = 0)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "MITM")]
        public string mitm { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "DATE")]
        public string date { get; set; }


        [DataMember(Order = 6)]
        [Column(Name = "MESS")]
        public string mess { get; set; }


        [DataMember(Order = 7)]
        [Column(Name = "USER")]
        public string user { get; set; }


        [DataMember(Order = 8)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 9)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }                                                                                                                                                                                                                                                                                                                                                

        public Ent_tticol020()
        {
            pdno = string.Empty;
            mitm = string.Empty;
            dsca = string.Empty;
            qtdl = 0;
            cuni = string.Empty;
            date = string.Empty;
            mess = string.Empty;
            user = string.Empty;
            refcntd = 0;
            refcntu = 0;
            idrecord = string.Empty;
        }

        public Ent_tticol020(string ppdno, string pmitm, string pdsca, decimal pqtdl, string pcuni,
                             string pdate, string pmess, string puser, int prefcntd, int prefcntu, string pidrecord)
        {
            this.pdno = ppdno;
            this.mitm = pmitm;
            this.dsca = pdsca;
            this.qtdl = pqtdl;
            this.cuni = pcuni;
            this.date = pdate;
            this.mess = pmess;
            this.user = puser;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.idrecord = pidrecord;
        }
    }
}
