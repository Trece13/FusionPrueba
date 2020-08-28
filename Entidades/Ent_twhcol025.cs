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
    public class Ent_twhcol025
    {
        [DataMember(Order = 0)]
        [Column(Name = "PAID")]
        public string PAID { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "ITEM")]
        public string ITEM { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "LOCA")]
        public string LOCA { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "CLOT")]
        public string CLOT { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "QTYA")]
        public int QTYA { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "UNIT")]
        public string UNIT { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "DATE")]
        public string DATE { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "LOGN")]
        public string LOGN { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CDIS")]
        public string CDIS { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "EMNO")]
        public string EMNO { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "PROC")]
        public int PROC { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTD")]
        public int REFCNTD { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "REFCNTU")]
        public int REFCNTU { get; set; }


        //T$PAID, T$ITEM, T$LOCA, T$CLOT, T$QTYA, T$UNIT, T$DATE, T$LOGN, T$EMNO, T$PROC, T$REFCNTD,T$REFCNTU
        ////public Ent_twhcol025()
        ////{
        ////    this.PAID = string.Empty;
        ////    this.ITEM    = string.Empty;
        ////    this.LOCA = string.Empty;
        ////    this.CLOT    = string.Empty;
        ////    this.QTYA = 0;
        ////    this.UNIT = string.Empty;
        ////    this.DATE = string.Empty;
        ////    this.LOGN = string.Empty;
        ////    this.CDIS = string.Empty;
        ////    this.EMNO = string.Empty;
        ////    this.PROC   = 0;
        ////    this.REFCNTD = 0;
        ////    this.REFCNTU = 0;
        ////}
    }
}
