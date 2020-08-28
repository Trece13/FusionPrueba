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
    public class Ent_twhwmd200
    {
        [DataMember(Order = 0)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }
        [DataMember(Order = 1)]
        public string sloc { get; set; }
        [DataMember(Order = 2)]
        public bool Error { get; set; }
        [DataMember(Order = 3)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 4)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 5)]
        public string SuccessMsg { get; set; }

        public Ent_twhwmd200()
        {
            cwar = string.Empty;
        }


        public Ent_twhwmd200(string _cwar)
        {
            this.cwar = _cwar;
        }
        [DataMember(Order = 6)]
        public bool error { get; set; }
        [DataMember(Order = 7)]
        public string typeMsgJs { get; set; }
        [DataMember(Order = 8)]
        public string errorMsg { get; set; }
    }
}
