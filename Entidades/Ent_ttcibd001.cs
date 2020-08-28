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
    public class Ent_ttcibd001
    {
        [DataMember(Order = 0)]
        [Column(Name = "ITEM")]
        public string item { get; set; }
        [DataMember(Order = 1)]
        public string dsca { get; set; }
        [DataMember(Order = 2)]
        public string kltc { get; set; }
        [DataMember(Order = 3)]
        public string cuni { get; set; }
        [DataMember(Order = 4)]
        public string kitm { get; set; }
        [DataMember(Order = 5)]
        public bool Error { get; set; }
        [DataMember(Order = 6)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 7)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 8)]
        public string SuccessMsg { get; set; }
        [DataMember(Order = 9)]
        public string cpcl { get; set; }

        public Ent_ttcibd001()
        {
            item = string.Empty;
        }

        public Ent_ttcibd001(string _item)
        {
            this.item = _item;
        }

        public string typeMsgJs { get; set; }

        public string errorMsg { get; set; }

        public bool error { get; set; }
    }
}


