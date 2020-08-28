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
    public class Ent_twhinr140
    {
        [DataMember(Order = 0)]
        [Column(Name = "LOCA")]
        public string loca { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }
        [DataMember(Order = 2)]
        public bool Error { get; set; }
        [DataMember(Order = 3)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 4)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 5)]
        public string SuccessMsg { get; set; }

        public Ent_twhinr140()
        {
            loca = string.Empty;
            clot = string.Empty;
        }

        public Ent_twhinr140(string _loca, string _clot)
        {
            this.loca = _loca;
            this.clot = _clot;
        }
        [DataMember(Order = 6)]
        public decimal stks { get; set; }
        [DataMember(Order = 7)]
        public string errorMsg { get; set; }
        [DataMember(Order = 8)]
        public string typeMsgJs { get; set; }
        [DataMember(Order = 9)]
        public bool error { get; set; }
    }
}
