using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol120
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
        [Column(Name="IDTR")]
        public string idtr { get; set; }

        [DataMember(Order=5)]
        [Column(Name="WHSO")]
        public string whso { get; set; }

        [DataMember(Order=6)]
        [Column(Name="WHTA")]
        public string whta { get; set; }

        [DataMember(Order=7)]
        [Column(Name="SHPO")]
        public string shpo { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "RCNO")]
        public string rcno { get; set; }

        [DataMember(Order=9)]
        [Column(Name="ENPI")]
        public int enpi { get; set; }

        [DataMember(Order=10)]
        [Column(Name="ENRE")]
        public int enre { get; set; }

        [DataMember(Order=11)]
        [Column(Name="PRNT")]
        public int prnt { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "PROO")]
        public int proo { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "PROR")]
        public int pror { get; set; }

        [DataMember(Order=14)]
        [Column(Name="MES1")]
        public string mes1 { get; set; }

        [DataMember(Order=15)]
        [Column(Name="MES2")]
        public string mes2 { get; set; }

        [DataMember(Order=16)]
        [Column(Name="REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order=17)]
        [Column(Name="REFCNTU")]
        public int refcntu { get; set; }

        public Ent_twhcol120() 
        {
            unid = String.Empty;
            date = String.Empty;
            logn = String.Empty;
            idtr = String.Empty;
            whso = String.Empty;
            whta = String.Empty;
            shpo = String.Empty;
            enpi = 0;
            enre = 0;
            rcno = String.Empty;
            prnt = 0;
            proo = 0;
            pror = 0;
            mes1 = String.Empty;
            mes2 = String.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_twhcol120(Ent_twhcol120 datatwhcol120) 
        {
            this.unid = datatwhcol120.unid;
            this.date = datatwhcol120.date;
            this.logn = datatwhcol120.logn;
            this.idtr = datatwhcol120.idtr;
            this.whso = datatwhcol120.whso;
            this.whta = datatwhcol120.whta;
            this.shpo = datatwhcol120.shpo;
            this.enpi = datatwhcol120.enpi;
            this.enre = datatwhcol120.enre;
            this.rcno = datatwhcol120.rcno;
            this.prnt = datatwhcol120.prnt;
            this.proo = datatwhcol120.proo;
            this.pror = datatwhcol120.pror;
            this.mes1 = datatwhcol120.mes1;
            this.mes2 = datatwhcol120.mes2;
            this.refcntd = datatwhcol120.refcntd;
            this.refcntu = datatwhcol120.refcntu;
        }
    }
}
