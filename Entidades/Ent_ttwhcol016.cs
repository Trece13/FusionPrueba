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
    public class Ent_ttwhcol016
    {
        [DataMember(Order = 0)]
        [Column(Name = "ZONE")]
        public string zone { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "LABL")]
        public string labl { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "QTYR")]
        public decimal qtyr { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 10)]
        public string sloc { get; set; }
        [DataMember(Order = 11)]
        public bool Error { get; set; }
        [DataMember(Order = 12)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 13)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 14)]
        public string SuccessMsg { get; set; }
        [DataMember(Order = 15)]
        public string dsca { get; set; }

        public Ent_ttwhcol016()
        {
            zone = string.Empty;
            labl = string.Empty;
            item = string.Empty;
            cwar = string.Empty;
            clot = string.Empty;
            qtyr = 0;
            logn = string.Empty;
            date = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }


        public Ent_ttwhcol016(string pzone, string plabl, string pitem, string pcwar, string pclot, decimal pqtyr,
                              string plogn, string pdate, int prefcntd, int prefcntu)
        {
            this.zone = pzone;
            this.labl = plabl;
            this.item = pitem;
            this.cwar = pcwar;
            this.clot = pclot;
            this.qtyr = pqtyr;
            this.logn = plogn;
            this.date = pdate;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
        }
        [DataMember(Order = 16)]
        public string typeMsgJs { get; set; }
        [DataMember(Order = 17)]
        public bool error { get; set; }
        [DataMember(Order = 18)]
        public string errorMsg { get; set; }
    }
}




