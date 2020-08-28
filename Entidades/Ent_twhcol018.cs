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
    public class  Ent_twhcol018
    {
        [DataMember(Order = 0)]
        [Column(Name = "TGID")]
        public string tgid { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "ITEM")]
        public string item { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "QTDL")]
        public decimal qtdl { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 6)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }
        
        public Ent_twhcol018()
        {
            tgid = string.Empty;
            item = string.Empty;
            clot = string.Empty;
            qtdl = 0;
            cuni = string.Empty;
            refcntd = 0;
            refcntu = 0;
            orno = string.Empty;
        }
        public Ent_twhcol018(string ptgid, string pitem, string pclot, decimal pqtdl, string pcuni, int prefcntd, int prefcntu, string _orno)
        { 
            this.tgid = ptgid;    
            this.item = pitem;    
            this.clot = pclot;    
            this.qtdl = pqtdl;    
            this.cuni = pcuni;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.orno = _orno;
        }
    }
}
