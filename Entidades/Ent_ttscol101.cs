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
    public class Ent_ttscol101
    {
        [DataMember(Order = 0)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "ITEM")]
        public string item { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "QDEL")]
        public decimal qdel { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }


        [DataMember(Order = 6)]
        [Column(Name = "DATE")]
        public string date { get; set; }


        [DataMember(Order = 7)]
        [Column(Name = "CONF")]
        public int conf { get; set; }


        [DataMember(Order = 8)]
        [Column(Name = "CUSR")]
        public string cusr { get; set; }


        [DataMember(Order = 9)]
        [Column(Name = "CDAT")]
        public string cdat { get; set; }


        [DataMember(Order = 10)]
        [Column(Name = "MESS")]
        public string mess { get; set; }


        [DataMember(Order = 11)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 12)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_ttscol101()
        {
            orno = string.Empty;
            item = string.Empty;
            loca = string.Empty;
            cwar = string.Empty;
            qdel = 0;
            logn = string.Empty;
            date = string.Empty;
            conf = 0;
            cusr = string.Empty;
            cdat = string.Empty;
            mess = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }


        public Ent_ttscol101(string porno, string pitem, string ploca, string pcwar, decimal pqdel, string plogn,
                             string pdate, int pconf, string pcusr, string pcdat, string pmess, int prefcntd, int prefcntu)
        {
            this.orno = porno;
            this.item = pitem;
            this.loca = ploca;
            this.cwar = pcwar;
            this.qdel = pqdel;
            this.logn = plogn;
            this.date = pdate;
            this.conf = pconf;
            this.cusr = pcusr;
            this.cdat = pcdat;
            this.mess = pmess;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
        }
    }
}
