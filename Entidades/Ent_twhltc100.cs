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
    public class Ent_twhltc100
    {
        [DataMember(Order = 0)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }


        public Ent_twhltc100()
        {
            item = string.Empty;
            clot = string.Empty;
        }


        public Ent_twhltc100(string _item, string _clot)
        {
            this.item = _item;
            this.clot = _clot;
        }
    }
}
