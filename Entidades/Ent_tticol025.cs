using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol025
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
        [Column(Name = "QTDL")]
        public float qtdl { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_tticol025() 
        {
            pdno = String.Empty;
            sqnb = 0;
            mitm = String.Empty;
            dsca = String.Empty;
            qtdl = 0.0F;
            cuni = String.Empty;
            date = String.Empty;
            mess = " ";
            user = String.Empty;
            refcntd = 0;
            refcntu = 0;
        }
    }
}
