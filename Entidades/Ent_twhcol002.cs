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
    public class Ent_twhcol002
    {
        [DataMember(Order = 0)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "COUN")]
        public string coun { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }


        public Ent_twhcol002()
        {
            cwar = string.Empty;
            coun = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_twhcol002(string _cwar, string _coun, int _refcntd, int _refcntu)
        {
            this.cwar = _cwar;
            this.coun = _coun;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;
        }
    }
}
