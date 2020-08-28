using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol010
    {
        [DataMember(Order=0)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order=1)]
        [Column(Name = "SQNB")]
        public int sqnb { get; set; }

        [DataMember(Order=2)]
        [Column(Name = "MITM")]
        public string mitm { get; set; }

        [DataMember(Order=3)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }

        [DataMember(Order=4)]
        [Column(Name = "CWOR")]
        public string cwor { get; set; }

        [DataMember(Order=5)]
        [Column(Name = "LOOR")]
        public string loor { get; set; }

        [DataMember(Order=6)]
        [Column(Name = "CWDE")]
        public string cwde { get; set; }

        [DataMember(Order=7)]
        [Column(Name = "LODE")]
        public string lode { get; set; }

        [DataMember(Order=8)]
        [Column(Name = "QTDL")]
        public double qtdl { get; set; }

        [DataMember(Order=9)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order=10)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order=11)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order=12)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order=13)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order=14)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }
    }
}
