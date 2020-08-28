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
    public class Ent_ttccol301
    {
    
        [DataMember(Order = 0)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "FEIN")]
        public string fein { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "COME")]
        public string come { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        public Ent_ttccol301()
        {
            user = string.Empty;
            fein = string.Empty;
            come = string.Empty;
            refcntd = 2;
            refcntu = 2;
            idrecord = string.Empty;
        }
        
        public Ent_ttccol301 (string puser,string pfein,string pcome,int prefcntd,int prefcntu, string pidrecord)
        {
            this.user    = puser;
            this.fein    = pfein;
            this.come    = pcome;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.idrecord = pidrecord;
        }
    }
}
