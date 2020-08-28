using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol032
    {
        [DataMember(Order = 0)]
        [Column(Name = "OORG")]
        public int oorg { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "SQNB")]
        public string sqnb { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "OSET")]
        public int oset { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "PONO")]
        public int pono { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "SEQN")]
        public int seqn { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "MITM")]
        public string mitm { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 15)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 16)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }
    }
}
