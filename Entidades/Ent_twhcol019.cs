using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol019
    {
        [DataMember(Order = 0)]
        [Column(Name = "PAID")]
        public string PAID { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "SQNB")]
        public long SQNB	{ get; set; }
        
        [DataMember(Order = 2)]
        [Column(Name = "ZONE")]
        public string ZONE { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "CWAR")]
        public string CWAR { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "LOCA")]
        public string LOCA { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "ITEM")]
        public string ITEM { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "CLOT")]
        public string CLOT { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "QTDL")]
        public decimal QTDL { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CUNI")]
        public string CUNI { get; set; }
        
        [DataMember(Order = 9)]
        [Column(Name = "LOGN")]
        public string LOGN { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "DATE")]
        public DateTime DATE	{ get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "COUN")]
        public int COUN { get; set; }
        
        [DataMember(Order = 12)]
        [Column(Name = "PROC")]
        public int PROC { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "REFCNTD")]
        public int REFCNTD { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "REFCNTU")]
        public int REFCNTU { get; set; }

        [DataMember(Order = 15)]
        public bool error { get; set; }

        [DataMember(Order = 16)]
        public string typeMsgJs { get; set; }

        [DataMember(Order = 17)]
        public string errorMsg { get; set; }
        [DataMember(Order = 18)]
        public string SuccessMsg { get; set; }
    }
}



