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
    public class Ent_tticol125
    {

        [DataMember(Order = 0)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "PONO")]
        public int pono { get; set; }

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
        [Column(Name = "REQT")]
        public decimal reqt { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "PRIN")]
        public int prin { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "CONF")]
        public int conf { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "REFCNTD")]
        public string refcntd { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTU")]
        public string refcntu { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "MESS")]
        public string mess { get; set; }


        [DataMember(Order = 13)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "PAID")]
        public string paid { get; set; }

        [DataMember(Order = 15)]
        public bool Error { get; set; }
        [DataMember(Order = 16)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 17)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 18)]
        public string SuccessMsg { get; set; }
        [DataMember(Order = 19)]
        public string dsca { get; set; }
        [DataMember(Order = 20)]
        public string cuni { get; set; }

        public Ent_tticol125()
        {

            pdno = string.Empty;
            pono = 0;
            item = string.Empty;
            cwar = string.Empty;
            clot = string.Empty;
            reqt = 0;
            user = string.Empty;
            date = string.Empty;
            prin = 0;
            conf = 0;
            refcntd = string.Empty;
            refcntu = string.Empty;
            idrecord = string.Empty;
            paid = string.Empty;
        }

        public Ent_tticol125(string ppdno, int ppono, string pitem, string pcwar, string pclot, decimal preqt, string puser, string pdate,
                                 int pprin, int pconf, string prefcntd, string prefcntu, string pmess, string pidRecord, string ppaid)
        {

            this.pdno = ppdno;
            this.pono = ppono;
            this.item = pitem;
            this.cwar = pcwar;
            this.clot = pclot;
            this.reqt = preqt;
            this.user = puser;
            this.date = pdate;
            this.prin = pprin;
            this.conf = pconf;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.mess = pmess;
            this.idrecord = pidRecord;
            this.paid = ppaid;
        }




        [DataMember(Order = 21)]
        public string tbl { get; set; }
        [DataMember(Order = 22)]
        public string stat { get; set; }
        [DataMember(Order = 23)]
        public string qtyc { get; set; }
        [DataMember(Order = 24)]
        public string qtya { get; set; }
        [DataMember(Order = 25)]
        public bool error { get; set; }
        [DataMember(Order = 26)]
        public string typeMsgJs { get; set; }
        [DataMember(Order = 27)]
        public string errorMsg { get; set; }
    }
}


