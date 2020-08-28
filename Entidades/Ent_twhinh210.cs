using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhinh210
    {
        [DataMember(Order = 0)]
        [Column(Name="RCNO")]
        public string rcno { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "RCUN")]
        public string rcun { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "OORG")]
        public string oorg { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        //Propiedades referentes a otra tabla
        public string ttcibd001_dsca { get; set; }
    }
}
