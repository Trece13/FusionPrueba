using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol122
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
        [Column(Name="LOSO")]
        public string loso { get; set; }

        [DataMember(Order=5)]
        [Column(Name="PAID")]
        public string paid { get; set; }

        [DataMember(Order=6)]
        [Column(Name="ITEM")]
        public string item { get; set; }

        [DataMember(Order=7)]
        [Column(Name="CLOT")]
        public string clot { get; set; }

        [DataMember(Order=8)]
        [Column(Name="QTDT")]
        public float qtdt { get; set; }

        [DataMember(Order=9)]
        [Column(Name="PROC")]
        public int proc { get; set; }

        [DataMember(Order=10)]
        [Column(Name="MES1")]
        public string mes1 { get; set; }

        [DataMember(Order=11)]
        [Column(Name="DAFS")]
        public string dafs { get; set; }

        [DataMember(Order=12)]
        [Column(Name="REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order=13)]
        [Column(Name="REFCNTU")]
        public int refcntu { get; set; }

        public Ent_twhcol122() 
        {
            unid = String.Empty;
            date = String.Empty;
            logn = String.Empty;
            loso = String.Empty;
            paid = String.Empty;
            item = String.Empty;
            clot = String.Empty;
            qtdt = 0.0F;
            proc = 0;
            mes1 = String.Empty;
            dafs = String.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_twhcol122(Ent_twhcol122 datatwhcol122) 
        {
            this.unid = datatwhcol122.unid;
            this.date = datatwhcol122.date;
            this.logn = datatwhcol122.logn;
            this.loso = datatwhcol122.loso;
            this.paid = datatwhcol122.paid;
            this.item = datatwhcol122.item;
            this.clot = datatwhcol122.clot;
            this.qtdt = datatwhcol122.qtdt;
            this.proc = datatwhcol122.proc;
            this.mes1 = datatwhcol122.mes1;
            this.dafs = datatwhcol122.dafs;
            this.refcntd = datatwhcol122.refcntd;
            this.refcntu = datatwhcol122.refcntu;
        }
    }
}
