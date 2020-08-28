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
    public class Ent_ttccol300
    {
        [DataMember(Order = 0)]
        [Column(Name = "USER")]
        public string user { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "NAMA")]
        public string nama { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "PASS")]
        public string pass { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "UFIN")]
        public string ufin { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "ROLE")]
        public int role { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "SHIF")]
        public string shif { get; set; }

        public Ent_ttccol300()
        {
            user = string.Empty;
            nama = string.Empty;
            pass = string.Empty;
            ufin = string.Empty;
            refcntd = 0;
            refcntu = 0;
            role = 0;
            shif = "";
        }

        public Ent_ttccol300 (string puser,string pnama, string ppass, string pufin, int prefcntd, int prefcntu, int role, string shif)
        {
            this.user = puser;
            this.nama = pnama;
            this.pass = ppass;
            this.ufin = pufin;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.role   = role;
            this.shif   = shif;
        }
    }
}
