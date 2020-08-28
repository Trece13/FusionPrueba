using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol119
    {
        [DataMember(Order=1)]
        [Column(Name="CWAR")]
        public string cwar { get; set; }

        [DataMember(Order=2)]
        [Column(Name="LOCA")]
        public string loca { get; set; }

        [DataMember(Order=3)]
        [Column(Name="TRDT")]
        public string trdt { get; set; }

        [DataMember(Order=4)]
        [Column(Name="REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order=5)]
        [Column(Name="REFCNTU")]
        public int refcntu { get; set; }

        public Ent_twhcol119() 
        {
            cwar = String.Empty;
            loca = String.Empty;
            trdt = String.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_twhcol119(Ent_twhcol119 datatwhcol119) 
        {
            this.cwar = datatwhcol119.cwar;
            this.loca = datatwhcol119.loca;
            this.trdt = datatwhcol119.trdt;
            this.refcntd = datatwhcol119.refcntd;
            this.refcntu = datatwhcol119.refcntu;
        }
    }
}
