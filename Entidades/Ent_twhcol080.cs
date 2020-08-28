using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol080
    {
        [DataMember(Order=0)]
        [Column(Name="SOUR")]
        public int sour { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "CONJ")]
        public int conj { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "PONO")]
        public int pono { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "SQNB")]
        public int sqnb { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "SERN")]
        public int sern { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "RCNO")]
        public string rcno { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "QANA")]
        public double qana { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "HABL")]
        public double habl { get; set; }

        [DataMember(Order = 15)]
        [Column(Name = "ESTI")]
        public int esti { get; set; }

        [DataMember(Order = 16)]
        [Column(Name = "INDT")]
        public string indt { get; set; }

        [DataMember(Order = 17)]
        [Column(Name = "PRDT")]
        public string prdt { get; set; }

        [DataMember(Order = 18)]
        [Column(Name = "DIFE")]
        public int dife { get; set; }

        [DataMember(Order = 19)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 20)]
        [Column(Name = "CONF")]
        public int conf { get; set; }

        [DataMember(Order = 21)]
        [Column(Name = "ORIG")]
        public int orig { get; set; }

        [DataMember(Order = 22)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 23)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_twhcol080() 
        {
            sour = 0;
            orno = String.Empty;
            conj = 0;
            pono = 0;
            sqnb = 0;
            sern = 0;
            rcno = String.Empty;
            cwar = " ";
            loca = " ";
            item = String.Empty;
            qana = 0;
            cuni = String.Empty;
            clot = String.Empty;
            logn = String.Empty;
            habl = 0;
            esti = 0;
            dife = 2;
            proc = 2;
            conf = 2;
            orig = 1;
            refcntd = 0;
            refcntu = 0;
        }
    }
}
