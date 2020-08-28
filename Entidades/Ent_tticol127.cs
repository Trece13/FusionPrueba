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
    public class Ent_tticol127
    {
        
        [DataMember(Order = 0)]
        [Column(Name = "USER")]
        public string user {get; set;}

        [DataMember(Order = 1)]
        [Column(Name = "CWAR")]
        public string cwar	{get; set;}

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "DSCA")]
        public string dsca { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "LOTE")]
        public string lote { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "REFCNTD")]
        public string refcntd  {get; set;}

        [DataMember(Order = 6)]
        [Column(Name = "REFCNTU")]
        public string refcntu {get; set;}

        [DataMember(Order = 7)]
        [Column(Name = "KLTC")]
        public string kltc { get; set; }

        public Ent_tticol127()
        {

            user = string.Empty;
            user = "";
            cwar = string.Empty;
            item = string.Empty;
            dsca = string.Empty;
            lote = string.Empty;
            refcntd = string.Empty;
            refcntu = string.Empty;
        }

        public Ent_tticol127(string puser, string pcwar, string pitem, string pdsca, string plote, string prefcntd, string prefcntu)
        {

            this.user = puser;
            this.cwar = pcwar;
            this.item = pitem;
            this.dsca = pdsca;
            this.lote = plote;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
        }        
    }
}
