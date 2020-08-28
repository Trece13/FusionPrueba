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
    public class Ent_twhwmd300
    {
        [DataMember(Order = 0)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }


        public Ent_twhwmd300()
        {
            cwar = string.Empty;
            loca = string.Empty;
            pdno = string.Empty;
        }


        public Ent_twhwmd300(string _cwar, string _loca, string _pdno)
        {
            this.cwar = _cwar;
            this.loca = _loca;
            this.pdno = _pdno;
        }

    }
}
