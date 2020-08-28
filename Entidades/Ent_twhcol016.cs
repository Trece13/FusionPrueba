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
    public class Ent_twhcol016
    {
        public string ZONE { get; set; }
        public string LABL { get; set; }
        public string ITEM { get; set; }
        public string CWAR { get; set; }
        public string CLOT { get; set; }
        public string QTYR { get; set; }
        public string LOGN { get; set; }
        public string DATE { get; set; }
        public string REFCNTD { get; set; }
        public string REFCNTU { get; set; }


        public Ent_twhcol016()
        {
            this.ZONE    = string.Empty;
            this.LABL    = string.Empty;
            this.ITEM    = string.Empty;
            this.CWAR    = string.Empty;
            this.CLOT    = string.Empty;
            this.QTYR    = string.Empty;
            this.LOGN    = string.Empty;
            this.DATE    = string.Empty;
            this.REFCNTD = string.Empty;
            this.REFCNTU = string.Empty;
        }
    }
}
