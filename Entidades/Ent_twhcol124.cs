using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol124
    {
        [DataMember(Order=1)]
        [Column(Name="UNID")]
        public string unid { get; set; }

        [DataMember(Order=2)]
        [Column(Name="DATE")]
        public string date { get; set; }

        [DataMember(Order=3)]
        [Column(Name="LOGN")]
        public string logn { get; set; }

        [DataMember(Order=4)]
        [Column(Name="PAID")]
        public string paid { get; set; }

        [DataMember(Order=5)]
        [Column(Name="ITEM")]
        public string item { get; set; }

        [DataMember(Order=6)]
        [Column(Name="CLOT")]
        public string clot { get; set; }

        [DataMember(Order=7)]
        [Column(Name="QTDT")]
        public double qtdt { get; set; }

        [DataMember(Order=8)]
        [Column(Name="PROC")]
        public int proc { get; set; }

        [DataMember(Order=9)]
        [Column(Name="MES1")]
        public string mes1 { get; set; }

        [DataMember(Order=10)]
        [Column(Name="DAFS")]
        public string dafs { get; set; }

        [DataMember(Order=11)]
        [Column(Name="REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order=12)]
        [Column(Name="REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "LOCS")]
        public int locs { get; set; }

        public Ent_twhcol124() 
        {
            unid = String.Empty;
            date = String.Empty;
            logn = String.Empty;
            paid = String.Empty;
            item = String.Empty;
            clot = String.Empty;
            qtdt = 0.0;
            proc = 0;
            mes1 = String.Empty;
            dafs = String.Empty;
            refcntd = 0;
            refcntu = 0;
            loca = " ";
            locs = 2;
        }

        public Ent_twhcol124(Ent_twhcol124 datatwhcol124) 
        {
            this.unid = datatwhcol124.unid;
            this.date = datatwhcol124.date;
            this.logn = datatwhcol124.logn;
            this.paid = datatwhcol124.paid;
            this.item = datatwhcol124.item;
            this.clot = datatwhcol124.clot;
            this.qtdt = datatwhcol124.qtdt;
            this.proc = datatwhcol124.proc;
            this.mes1 = datatwhcol124.mes1;
            this.dafs = datatwhcol124.dafs;
            this.refcntd = datatwhcol124.refcntd;
            this.refcntu = datatwhcol124.refcntu;
        }
    }
}
