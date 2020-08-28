using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol095
    {
        [DataMember(Order = 0)]
        [Column(Name = "MCNO")]
        public string mcno { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "SEQN")]
        public int seqn { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "UNIT")]
        public string unit { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "QANA")]
        public double qana { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "MESS")]
        public string mess { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_tticol095() 
        {
            mcno = String.Empty;
            seqn = 0;
            item = String.Empty;
            cwar = String.Empty;
            unit = String.Empty;
            qana = 0;
            logn = String.Empty;
            date = String.Empty;
            proc = 0;
            mess = " ";
            refcntd = 0;
            refcntu = 0;
        }
    }
}
